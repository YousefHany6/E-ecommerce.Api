using E_ecommerce.Data.Constant;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.Order.Response
{
	public class OrderResponse
	{
		public int ID { get; set; }
		public DateTime OrderDate { get; set; }
		public string CustomerName { get; set; }
		public decimal TotalPrice { get; set; }
		public string OrderStatus { get; set; }
		public List<ProductOrderResponse>?Products_IN_Order { get; set; }	
	}
	public class ProductOrderResponse
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public int ProductQuantity { get; set; }
		public decimal Price { get; set; }
	}
}
