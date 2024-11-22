﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class FavoriteCart
	{
		[Key]
		public int ID { get; set; }
		public int CustomerID { get; set; }
		[ForeignKey(nameof(CustomerID))]
		public virtual User? Customer { get; set; }
		public virtual ICollection<FavoriteCartProduct>? FavoriteCartProducts { get; set; }

		public FavoriteCart()
		{
			FavoriteCartProducts= new HashSet<FavoriteCartProduct>();
		}

	}
}