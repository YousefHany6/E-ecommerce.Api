using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class ProductsOrder
	{
		public int OrderID { get; set; }
		public int ProductID { get; set; }
		public int ProductQuantity { get; set; }
		public decimal Price { get; set; }
		[ForeignKey(nameof(OrderID))]
		public virtual Order? Order { get; set; }
		[ForeignKey(nameof(ProductID))]
		public virtual Product? Product { get; set; }


	}
}
