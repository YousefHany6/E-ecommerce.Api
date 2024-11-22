using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.ProductModel
{
	public class ProductModelResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Brand { get; set; }
		public decimal FinalPrice { get; set; }
		public int Quantity { get; set; }
		public bool HasDiscount { get; set; } = false;
		public bool InStock => Quantity > 0;
		public string? DiscountName { get; set; }
		public string? Discountpercentage { get; set; }
		public string? CategoryName { get; set; }
		public string? UserCreateProductName { get; set; }
		public DateTime? DiscountExpireDate { get; set; }
		public DateTime CreateDate { get; set; }
		public HashSet<string>? photos { get; set; }
	}
}
