using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.CategoryModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Command.Models
{
	public class Edit_CategoryModel: IRequest<Response<ResponseCategoryModel>>
	{
		public int CategoryID { get; set; }
		public EditCategoryModel model { get; set; }
		public Edit_CategoryModel(int categoryID, EditCategoryModel model)
		{
			CategoryID = categoryID;
			this.model = model;
		}
	}
}
