using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.CategoryModels
{
	public class ResponseCategoryModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? ImageUrl { get; set; }
		public string? Description { get; set; }
		public ICollection<ProductModel.ProductModelResponse>? Products { get; set; } = new HashSet<ProductModel.ProductModelResponse>();

	}
}
