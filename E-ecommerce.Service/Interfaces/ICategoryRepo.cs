using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.CategoryModels;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface ICategoryRepo
	{
		Task<IQueryable<Category>> GetCategories(EnumOrderCategory? order,string? search);
		Task<ErrorCategory> GetByIdCategory(int CategoryId);
		Task<ErrorCategory> AddCategory(AddCategoryModel model);
		Task<ErrorCategory> EditCategory(int CategoryId,EditCategoryModel model);
		Task<ErrorCategory> DeleteCategory(int CategoryId);
	}
}
