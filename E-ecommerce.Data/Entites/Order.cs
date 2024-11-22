using E_ecommerce.Data.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class Order
	{
		[Key]
		public int ID { get; set; }
		public DateTime OrderDate { get; set; } = DateTime.UtcNow.ToLocalTime();
		public int CustomerID { get; set; }

		[ForeignKey(nameof(CustomerID))]
		public virtual User? Customer { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
		public OrderStatus OrderStatus { get; set; } = OrderStatus.pending;
		public virtual ICollection<ProductsOrder>? ProductsOrders { get; set; }
		public Order()
		{
			ProductsOrders = new HashSet<ProductsOrder>();
		}

	}
}
