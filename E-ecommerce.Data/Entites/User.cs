using E_ecommerce.Data.Constant;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public  class User:IdentityUser<int>
	{
		public User()
		{
			Products = new HashSet<Product>();
			Contacts = new HashSet<Contact>();
			Orders=new HashSet<Order>();
			UserAddresses=new HashSet<UserAddress>();
			UserPhoneNumbers=new HashSet<UserPhoneNumber>();
			RefreshTokens=new HashSet<RefreshToken>();
		}
		[Required,MaxLength(60)]
		public string FirstName { get; set; }
		[Required, MaxLength(60)]
		public string LastName { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		[Required]
		public Gender Gender { get; set; }

		public virtual ICollection<Product>? Products { get; set;}
		public virtual ICollection<Contact>? Contacts { get; set; }
		public virtual ICollection<Order>? Orders { get; set; }
		public virtual ICollection<UserAddress>? UserAddresses { get; set; }
		public virtual ICollection<UserPhoneNumber>? UserPhoneNumbers { get; set; }
		public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
	}
}
