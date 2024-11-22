using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class FavoriteCartProduct
	{

		public int FavoriteCartID { get; set; }

		public int ProductID { get; set; }
		[ForeignKey(nameof(ProductID))]
		public virtual Product? Product { get; set; }
		[ForeignKey(nameof(FavoriteCartID))]
		public virtual FavoriteCart? FavoriteCart { get; set; }
	}
}
