using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class Discount
	{
		public Discount()
		{
			Products = new HashSet<Product>();
		}
		[Key]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int DiscountPercentage { get; set; }
		public virtual ICollection<Product>? Products { get; set; }
	

	}
}
