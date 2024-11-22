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
	public class Add_CategoryModel:IRequest<Response<ResponseCategoryModel>>
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public IFormFile ImageFile { get; set; }

	}
}
