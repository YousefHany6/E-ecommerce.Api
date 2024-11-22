using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.CategoryModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Query.Models
{
	public class CategoryGetByIdModel : IRequest<Response<ResponseCategoryModel>>
	{
		public int categoryId
		{
			get; set;
		}
		public CategoryGetByIdModel(int categoryId)
		{
			this.categoryId = categoryId;
		}
	}
}
