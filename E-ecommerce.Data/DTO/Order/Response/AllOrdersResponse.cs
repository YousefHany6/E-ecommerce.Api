using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.Order.Response
{
	public class AllOrdersResponse
	{
		public int ID { get; set; }
		public DateTime OrderDate { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerId { get; set; }
		public decimal TotalPrice { get; set; }
		public string? OrderStatus { get; set; }
	}
}
