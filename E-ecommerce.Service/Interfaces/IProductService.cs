using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.DTO.ProductModel.Request;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IProductService
	{
		Task<ErrorProductResult> AddProduct(int userid, ProductModelRequest model);
		Task<ErrorProductResult> EditProduct(int ProductId, EditProductmodelRequest model);
		Task<ErrorProductResult> DeleteProduct(int ProductId);
		Task<ErrorProductResult> GetProductById(int ProductId);
		Task<IQueryable<Product>> GetAllProducts(ProductEnumOrder? order, string? search);
       
    }
}
