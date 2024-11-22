using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		[Required]
		public string Brand { get; set; }

		[Required]
		public decimal BasePrice { get; set; }  // Base price before any discounts

		[Required]
		public int Quantity { get; set; }

		public bool HasDiscount => Discount != null && DiscountExpireDate > DateTime.UtcNow;

		public bool InStock => Quantity > 0;

		public int CategoryID { get; set; }

		[ForeignKey(nameof(CategoryID))]
		public virtual Category? Category { get; set; }

		public int? DiscountID { get; set; }

		[ForeignKey(nameof(DiscountID))]
		public virtual Discount? Discount { get; set; }

		public DateTime CreateDate { get; set; } = DateTime.UtcNow.ToLocalTime();
		public DateTime? DiscountExpireDate { get; set; }

		public int ManagerID { get; set; }

		[ForeignKey(nameof(ManagerID))]
		public virtual User? User { get; set; }

		public virtual ICollection<ProductPhoto>? ProductPhotos { get; set; }
		public virtual ICollection<ProductReview>? ProductReviews { get; set; }
		public virtual ICollection<ProductsOrder>? ProductsOrders { get; set; }
		public virtual ICollection<CartProduct>? CartProducts { get; set; }
		public virtual ICollection<FavoriteCartProduct>? FavoriteCartProducts { get; set; }

		public Product()
		{
			ProductPhotos = new HashSet<ProductPhoto>();
			ProductReviews = new HashSet<ProductReview>();
			ProductsOrders = new HashSet<ProductsOrder>();
			CartProducts = new HashSet<CartProduct>();
			FavoriteCartProducts = new HashSet<FavoriteCartProduct>();
		}

		[NotMapped]
		public decimal FinalPrice => HasDiscount ? BasePrice - (BasePrice * Discount.DiscountPercentage / 100) : BasePrice;
	}

}
