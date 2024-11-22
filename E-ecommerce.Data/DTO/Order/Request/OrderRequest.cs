using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.Order.Request
{
	public class OrderRequest
	{
		[Required]
		[MinLength(1, ErrorMessage = "The Products list must contain at least one product.")]
		public List<OrderProduct>? Products { get; set; }

		[Required, DataType(DataType.Text)]
		public List<string>? UserAddress { get; set; }

		[Required, DataType(DataType.PhoneNumber)]
		public List<string>? UserPhoneNumber { get; set; }
	}
	public class OrderProduct
	{
		public int ProductID { get; set; }

		public int ProductQuantity { get; set; }
	}
}
