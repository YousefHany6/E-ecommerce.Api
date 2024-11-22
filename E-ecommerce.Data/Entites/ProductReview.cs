using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class ProductReview
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Comment { get; set; }
		public int CustomerID { get; set; }
		[ForeignKey(nameof(CustomerID))]
		public virtual User? Customer { get; set; }
		public int ProductID { get; set; }
		[ForeignKey(nameof(ProductID))]
		public virtual Product? Product { get; set; }

	}
}
