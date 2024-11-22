using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.ProductModel.Request
{
	public class ProductModelRequest
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public string Brand { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public int CategoryID { get; set; }
		[Required]
		public List<IFormFile> ProductPhotos { get; set; }

	}
}
