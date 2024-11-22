using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class Category
	{
		public Category()
		{
			Products = new HashSet<Product>();
		}
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		public string? Description { get; set; }

		public virtual ICollection<Product>? Products { get; set; }
	}
}
