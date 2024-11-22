using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.Order.Response
{
	public class ErrorOrder
	{
		public string? Message { get; set; }
		public bool ok { get; set; }
		public E_ecommerce.Data.Entites.Order OrderResponse { get; set; }
	}
}
