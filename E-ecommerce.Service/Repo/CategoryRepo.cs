using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.CategoryModels;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class CategoryRepo : ICategoryRepo
	{
		private readonly IPhotoService PhotoRepo;
		private readonly IStringLocalizer<Resources> lo;
		private readonly ApplicationContext context;

		public CategoryRepo(
			IPhotoService PhotoRepo,
			IStringLocalizer<Resources> lo,
			ApplicationContext context

			)
		{
			this.PhotoRepo = PhotoRepo;
			this.lo = lo;
			this.context = context;
		}
		public async Task<ErrorCategory> AddCategory(AddCategoryModel model)
		{
			var catisexist = await context.Categories
																												.AsNoTracking()
																												.FirstOrDefaultAsync(s => s.Name.ToLower() == model.Name.ToLower())
																												;
			if (catisexist is not null)
			{
				return new ErrorCategory { Message = lo[ResourcesKeys.IsExisted] };
			}
			var catphotoname = await PhotoRepo.AddPhoto(model.ImageFile, DefaultPhoto.CategoryFolder);
			var cat = new Category
			{
				Name = model.Name,
				Description = model.Description,
				ImageUrl = catphotoname
			};
			try
			{
				await context.Categories.AddAsync(cat);
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				return new ErrorCategory { ok = false, Message = e.Message };
			}
			return new ErrorCategory { ok = true, cat = cat };
		}
		public async Task<ErrorCategory> DeleteCategory(int CategoryId)
		{
			var cat = await context.Categories.FindAsync(CategoryId);
			if (cat is null)
			{
				return new ErrorCategory { Message = lo[ResourcesKeys.NotFound] };
			}
			context.Categories.Remove(cat);
			await PhotoRepo.DeletePhoto(cat.ImageUrl, DefaultPhoto.CategoryFolder);
			await context.SaveChangesAsync();
			return new ErrorCategory { ok = true, cat = cat };
		}
		public async Task<ErrorCategory> GetByIdCategory(int CategoryId)
		{
			var cat = await context.Categories
																										.AsNoTracking()
																										.Include(s => s.Products)
																										.ThenInclude(s => s.Discount)
																										.FirstOrDefaultAsync(s => s.Id == CategoryId)
																										;
			if (cat is null)
			{
				return new ErrorCategory { Message = lo[ResourcesKeys.NotFound] };
			}
			return new ErrorCategory { ok = true, cat = cat };
		}//done
		public async Task<ErrorCategory> EditCategory(int CategoryId, EditCategoryModel model)
		{
			var Cate = await context.Categories.FindAsync(CategoryId);
			if (CategoryId != model.Id || model == null || Cate == null)
			{
				return new ErrorCategory { Message = lo[ResourcesKeys.NotFound] };
			}
			Cate.Name = model.Name;
			Cate.Description = model.Description;
			if (model.newimage != null && model.newimage.FileName.Equals(Cate.ImageUrl))
			{
				var del = await PhotoRepo.DeletePhoto(Cate.ImageUrl, DefaultPhoto.CategoryFolder);
				if (!del)
				{
					return new ErrorCategory { Message = lo[ResourcesKeys.BadRequest] };
				}
				Cate.ImageUrl = await PhotoRepo.AddPhoto(model.newimage, DefaultPhoto.CategoryFolder);
			}
			await context.SaveChangesAsync();
			return new ErrorCategory { ok = true, cat = Cate };
		}
public async Task<IQueryable<Category>> GetCategories(EnumOrderCategory? order, string? search)
{
    var query = context.Categories.AsNoTracking()
                                   .Include(s => s.Products)
                                   .ThenInclude(s => s.Discount)
																																			.AsQueryable()
																																			;

    // Apply search filter
    if (!string.IsNullOrEmpty(search))
    {
        query = query.Where(s => s.Name.Contains(search));
    }

    // Apply ordering
    switch (order)
    {
        case EnumOrderCategory.Id:
            query = query.OrderBy(x => x.Id);
            break;
        case EnumOrderCategory.Name:
            query = query.OrderBy(x => x.Name);
            break;
    }

    return query; 
}
	}
}
