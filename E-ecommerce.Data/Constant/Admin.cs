using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Constant
{
	public static class Admin
	{
		public static string Fname = "Yousef";
		public static string lname = "Hany";
		public static string Email = "adminjoo12@Admin.com";
		public static string Password = "Admin.123@#";
		public static Roles Role = Roles.SuperManager;
		public static string image = "DefaultMale.jpg";

		public static readonly HashSet<Roles> UserRoles = new HashSet<Roles>
		{
				Roles.SuperManager,
		Roles.Customer,
		Roles.Manager
		};
	}
}
