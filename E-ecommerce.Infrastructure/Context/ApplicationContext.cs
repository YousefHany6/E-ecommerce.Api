using E_ecommerce.Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Infrastructure.Context
{
	public class ApplicationContext : IdentityDbContext<User, Role, int, UserClaim, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<CartProduct>(entity =>
			{
				entity.HasOne(cp => cp.Product)
										.WithMany(p => p.CartProducts)
										.HasForeignKey(cp => cp.ProductId)
										.OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(cp => cp.Cart)
										.WithMany(c => c.CartProducts)
										.HasForeignKey(cp => cp.CartID)
										.OnDelete(DeleteBehavior.NoAction);
			});
			//---------------------------
			builder.Entity<FavoriteCartProduct>(entity =>
			{
				entity.HasOne(cp => cp.Product)
										.WithMany(p => p.FavoriteCartProducts)
										.HasForeignKey(cp => cp.ProductID)
										.OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(cp => cp.FavoriteCart)
										.WithMany(c => c.FavoriteCartProducts)
										.HasForeignKey(cp => cp.FavoriteCartID)
										.OnDelete(DeleteBehavior.NoAction);
			});
			//---------------------------

			builder.Entity<ProductReview>(entity =>
			{
				entity.HasOne(cp => cp.Product)
										.WithMany(p => p.ProductReviews)
										.HasForeignKey(cp => cp.ProductID)
										.OnDelete(DeleteBehavior.NoAction);
			});
			//---------------------------
			builder.Entity<ProductsOrder>(entity =>
			{
				entity.HasOne(cp => cp.Product)
										.WithMany(p => p.ProductsOrders)
										.HasForeignKey(cp => cp.ProductID)
										.OnDelete(DeleteBehavior.NoAction);

				entity.HasOne(cp => cp.Order)
										.WithMany(c => c.ProductsOrders)
										.HasForeignKey(cp => cp.OrderID)
										.OnDelete(DeleteBehavior.NoAction);
			});
			//---------------------------
			builder.Entity<Product>()
				.Property(s => s.BasePrice)
					.HasColumnType("decimal(18, 2)")
						.HasPrecision(18, 2);
			//---------------------------

			builder.Entity<CartProduct>()
.HasKey(key => new { key.ProductId, key.CartID });
			//---------------------------


			builder.Entity<ProductsOrder>()
	.HasKey(key => new { key.ProductID, key.OrderID });
			//---------------------------


			builder.Entity<FavoriteCartProduct>()
.HasKey(key => new { key.ProductID, key.FavoriteCartID });
			//---------------------------

			base.OnModelCreating(builder);
		}
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<User> Users { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartProduct> CartProducts { get; set; }
		public virtual DbSet<Contact> Contacts { get; set; }
		public virtual DbSet<Discount> Discounts { get; set; }
		public virtual DbSet<FavoriteCart> FavoriteCarts { get; set; }
		public virtual DbSet<FavoriteCartProduct> FavoriteCartProducts { get; set; }
		public virtual DbSet<ProductPhoto> ProductPhotos { get; set; }
		public virtual DbSet<ProductReview> ProductReviews { get; set; }
		public virtual DbSet<ProductsOrder> ProductsOrders { get; set; }
		public virtual DbSet<UserAddress> UserAddresses { get; set; }
		public virtual DbSet<OTP> OTPs { get; set; }
	}
}
