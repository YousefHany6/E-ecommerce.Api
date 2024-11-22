using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.CategoryModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Command.Models
{
	public class Delete_CategoryModel:IRequest<Response<ResponseCategoryModel>>
	{
		public int CategoryId { get; set; }
		public Delete_CategoryModel(int categoryId)
		{
			CategoryId = categoryId;
		}
	}
}
