using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class CartProduct
	{
		public int CartID { get; set; }

		public int ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		[InverseProperty("CartProducts")]
		public virtual Product? Product { get; set; }

		[ForeignKey(nameof(CartID))]
		[InverseProperty("CartProducts")]
		public virtual Cart? Cart { get; set; }
	}

}
