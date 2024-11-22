using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.DTO.ProductModel.Request;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
    public class ProductService : IProductService
    {
        private readonly ICategoryRepo categoryRepo;
        private readonly IStringLocalizer<Resources> lo;
        private readonly ApplicationContext context;
        private readonly IPhotoService photoRepo;
        private readonly IDiscountRepo discountRepo;

        public ProductService(ICategoryRepo categoryRepo,
            IStringLocalizer<Resources> lo,
            ApplicationContext context,
            IPhotoService PhotoRepo,
            IDiscountRepo discountRepo
            )
        {
            this.categoryRepo = categoryRepo;
            this.lo = lo;
            this.context = context;
            photoRepo = PhotoRepo;
            this.discountRepo = discountRepo;
        }
        public async Task<ErrorProductResult> AddProduct(int userid, ProductModelRequest model)
        {
            var cateisexist = await categoryRepo.GetByIdCategory(model.CategoryID);
            if (cateisexist.ok == false)
            {
                return new ErrorProductResult
                {
                    Ok = false,
                    Message_Error = "Category Not Found"
                };
            }
            using (var tran = await context.Database.BeginTransactionAsync())
            {
                var product = new Product
                {
                    Name = model.Name,
                    Brand = model.Brand,
                    CategoryID = model.CategoryID,
                    Description = model.Description,
                    Quantity = model.Quantity,
                    ManagerID = userid,
																	BasePrice = model.Price,
                };
                try
                {
                    await context.Products.AddAsync(product);
                    var photosnamelist = new List<ProductPhoto>();
                    foreach (var photo in model.ProductPhotos)
                    {
                        var name = await photoRepo.AddPhoto(photo, DefaultPhoto.ProductFolder);
                        photosnamelist.Add(new ProductPhoto { ProductID = product.Id, Url = name });
                    }
                    product.ProductPhotos.AddRange(photosnamelist);
                    await context.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new ErrorProductResult
                    {
                        Ok = true,
                        Product = product
                    };
                }

                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return new ErrorProductResult
                    {
                        Ok = false,
                        Message_Error = ex.Message
                    };
                }
            }
        }
        public async Task<ErrorProductResult> DeleteProduct(int ProductId)
        {
            var product = await context.Products                                                                                                                                .FirstOrDefaultAsync(s => s.Id == ProductId);
            if (product == null)
            {
                return new ErrorProductResult
                { Message_Error = lo[ResourcesKeys.NotFound] };
            }
            try
            {
                await DeleteProduct(product.ProductPhotos,
                                                                                                product.ProductReviews,
                                                                                                product.ProductsOrders,
                                                                                                product.FavoriteCartProducts,
                                                                                                product.CartProducts
                                                                                                );
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ErrorProductResult
                { Message_Error = ex.Message };
            }
            return new ErrorProductResult
            { Message_Error = lo[ResourcesKeys.DeletedSuccessfully], Product = product, Ok = true };

        }
        private async Task DeleteProduct(
            IEnumerable<ProductPhoto> photos,
            IEnumerable<ProductReview> reviews,
            IEnumerable<ProductsOrder> orders,
            IEnumerable<FavoriteCartProduct> favoriteCarts,
            IEnumerable<CartProduct> cartProducts
            )
        {
            foreach (var photo in photos)
            {
                await photoRepo.DeletePhoto(photo.Url, DefaultPhoto.ProductFolder);
                context.ProductPhotos.RemoveRange(photos);
            }
            if (reviews.Any())
            {
                context.ProductReviews.RemoveRange(reviews);
            }
            if (orders.Any())
            {
                context.ProductsOrders.RemoveRange(orders);
            }
            if (favoriteCarts.Any())
            {
                context.FavoriteCartProducts.RemoveRange(favoriteCarts);
            }
            if (cartProducts.Any())
            {
                context.CartProducts.RemoveRange(cartProducts);
            }
        }

        public async Task<ErrorProductResult> EditProduct(int ProductId, EditProductmodelRequest model)
        {
            var product = await context.Products
                                                                                                                                    .Include(s => s.Category)
                                                                                                                                                    .Include(s => s.ProductPhotos)
                                                                                                                                                    .Include(s => s.Discount)
                                                                                                                                    .FirstOrDefaultAsync(s => s.Id == ProductId);
            if (product == null || ProductId != model.Id)
            {
                return new ErrorProductResult
                { Message_Error = lo[ResourcesKeys.NotFound] };
            }
            product.Quantity = model.Quantity;
            product.Description = model.Description;
            product.BasePrice = model.Price;
            product.Brand = model.Brand;
            product.CategoryID = model.CategoryID;
            product.Name = model.Name;
            if (product.ProductPhotos.Select(s => s.Url).
                                                                    SequenceEqual(model.ProductPhotos.Select(s => s.FileName)))
            {
                foreach (var photo in product.ProductPhotos)
                {
                    await photoRepo.DeletePhoto(photo.Url, DefaultPhoto.ProductFolder);
                    context.ProductPhotos.RemoveRange(product.ProductPhotos);
                }
                foreach (var photo in model.ProductPhotos)
                {
                    var p = await photoRepo.AddPhoto(photo, DefaultPhoto.ProductFolder);
                    await context.ProductPhotos.AddAsync(new ProductPhoto { ProductID = product.Id, Url = p });
                }
            }
            await context.SaveChangesAsync();
            return new ErrorProductResult
            { Message_Error = lo[ResourcesKeys.Successfully], Product = product, Ok = true };

        }
        public async Task<IQueryable<Product>> GetAllProducts(ProductEnumOrder? order, string? search)
        {
            var query = context.Products.AsNoTracking()
                                                                                                                                                    .Include(s => s.Category)
                                                                                                                                                    .Include(s => s.ProductPhotos)
                                                                                                                                                    .Include(s => s.Discount)
                                                                                                                                                    .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            // Apply ordering
            switch (order)
            {
                case ProductEnumOrder.Id:
                    query = query.OrderBy(x => x.Id);
                    break;
                case ProductEnumOrder.Name:
                    query = query.OrderBy(x => x.Name);
                    break;
                case ProductEnumOrder.Brand:
                    query = query.OrderBy(x => x.Brand);
                    break;
                case ProductEnumOrder.Quantity:
                    query = query.OrderBy(x => x.Quantity);
                    break;
                case ProductEnumOrder.lowPriceTohighprice:
                    query = query.OrderBy(x => x.BasePrice);
                    break;
                case ProductEnumOrder.highPriceTolowprice:
                    query = query.OrderByDescending(x => x.BasePrice);
                    break;
            }

            return query;
        }
        public async Task<ErrorProductResult> GetProductById(int ProductId)
        {
            var product = await context.Products
                             .Include(s=>s.Category)
                             .Include(s=>s.Discount)
                             .Include(s=>s.ProductPhotos)
                             .Include(s=>s.User)
																													.AsNoTracking()
                             .FirstOrDefaultAsync(s => s.Id == ProductId);
            if (product == null)
            {
                return new ErrorProductResult
                { Message_Error = lo[ResourcesKeys.NotFound] };
            }
            return new ErrorProductResult
            { Message_Error = lo[ResourcesKeys.Successfully], Product = product, Ok = true };

        }

       }
}
