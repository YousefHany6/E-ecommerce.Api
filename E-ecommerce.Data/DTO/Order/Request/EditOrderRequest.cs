using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.Order.Request
{
	public class EditOrderRequest
	{
		[Required]
		public int OrderID { get; set; }
		[Required]
		public OrderRequest OrderRequest { get; set; }
	}
}
