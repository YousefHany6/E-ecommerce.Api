using E_ecommerce.Data.Constant;
using E_ecommerce.Data.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Seed
{
	public static class SeedRolesAndAdmin
	{
		public static async Task InitializeAsync( IServiceProvider serviceProvider)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

				await CreateRoles(roleManager);
				await AddSuperManager(userManager);
			}
		}

		private static async Task CreateRoles(RoleManager<Role> roleManager)
		{
			foreach (var roleName in Admin.UserRoles)
			{
				if (!await roleManager.RoleExistsAsync(roleName.ToString()))
				{
					var role = new Role()
					{
						Name = roleName.ToString(),
						ConcurrencyStamp = Guid.NewGuid().ToString()
					};
					await roleManager.CreateAsync(role);
				}
			}
		}


		private static async Task AddSuperManager(UserManager<User> userManager)
		{
			var superManager = await userManager.FindByEmailAsync(Admin.Email);

			if (superManager == null)
			{
				var user = new User
				{

					UserName = Admin.Email,
					Email = Admin.Email,
					EmailConfirmed = true,
					FirstName = Admin.Fname,
					LastName = Admin.lname,
					ImageUrl = Admin.image,
					Gender=Gender.Male
				};

				var result = await userManager.CreateAsync(user, Admin.Password);

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, Roles.SuperManager.ToString());
				}
				else
				{
					throw new ApplicationException($"Unable to create super manager. Errors: {string.Join(",", result.Errors)}");
				}
			}
		}

	}
}
