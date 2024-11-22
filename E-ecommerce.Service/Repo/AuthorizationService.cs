using E_ecommerce.Data.DTO.AuthorizationRequest;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class AuthorizationService : IAuthorizationService
	{
		private readonly RoleManager<Role> roleManager;
		private readonly IStringLocalizer<Resources> lo;
		private readonly UserManager<User> userManager;

		public AuthorizationService(
			RoleManager<Role> roleManager,
			IStringLocalizer<Resources> lo,
			UserManager<User> userManager
			)
		{
			this.roleManager = roleManager;
			this.lo = lo;
			this.userManager = userManager;
		}
		public async Task<ErrorRole> AddRoleAsync(string roleName)
		{
			//Check is Exist
			var isExist = await IsRoleExistByName(roleName);
			//Add Role
			if (isExist)
			{
				return new ErrorRole
				{
					message = roleName+" "+ lo[ResourcesKeys.IsExisted]
				};
			}
			var NewRole = new Role { Name = roleName };
			var r = await roleManager.CreateAsync(NewRole);
			if (!r.Succeeded)
			{
				return new ErrorRole { message = string.Join(",", r.Errors.ToString()) };
			}
			await roleManager.UpdateAsync(NewRole);
			return new ErrorRole { message = lo[ResourcesKeys.AddedSuccessfully], ok = true,Role=NewRole };
		}//C done
		public async Task<ErrorRole> DeleteRoleAsync(int roleId)
		{
			var role = await GetRoleById(roleId);
			if (role is null)
			{
				return new ErrorRole { message = lo[ResourcesKeys.NotFound] };
			}
			var r = await roleManager.DeleteAsync(role);
			if (!r.Succeeded)
			{
				return new ErrorRole { message = string.Join(",", r.Errors.ToString()) };
			}
			await roleManager.UpdateAsync(role);
			return new ErrorRole { message = lo[ResourcesKeys.DeletedSuccessfully], ok = true,Role=role };
		}//C done
		public async Task<ErrorRole> EditRoleAsync(EditRoleRequest request)
		{
			var role = await GetRoleById(request.old_Role_ID);
			if (role is null)
			{
				return new ErrorRole { message = lo[ResourcesKeys.NotFound] };
			}
			role.Name = request.New_Role_Name;
			await roleManager.UpdateAsync(role);
			return new ErrorRole { message = lo[ResourcesKeys.Successfully], ok = true };
		}//C done
		public async Task<Role> GetRoleById(int id)
		{
			var role = await roleManager.FindByIdAsync(id.ToString());
			return role == null ? null : role;
		}//q done
		public async Task<Role> GetRoleByName(string Name)
		{
			var role = await roleManager.FindByNameAsync(Name);
			return role == null ? null : role;
		}//q done
		public async Task<IEnumerable<Role>> GetRolesList()
		{

			return await roleManager.Roles.AsNoTracking().ToListAsync();
		}//q done
		public async Task<bool> IsRoleExistById(int roleId)
		{
			var Role = await roleManager.FindByIdAsync(roleId.ToString());
			return Role != null;
		}//q done
		public async Task<bool> IsRoleExistByName(string roleName)
		{
			var Role = await roleManager.RoleExistsAsync(roleName);
			return Role;
		}//q done
		public async Task<ManageUserRoles> ManageUserRolesData(User user)
		{
			var rolesfromuser = await userManager.GetRolesAsync(user);
			if (!rolesfromuser.Any())
			{
				return null;
			}
			var roles = await roleManager.Roles.ToListAsync();
			var UserRoles = new List<UserRole>();
			foreach (var role in roles)
			{
				foreach (var role_User in rolesfromuser)
				{
					if (role.Name == role_User)
					{
						UserRoles.Add(new UserRole { RoleId = role.Id, RoleName = role.Name });
					}
				}

			}
			return new ManageUserRoles { Roles = UserRoles, UserId = user.Id };
		}//q done
		public async Task<ErrorRole> AddUserInNewRole(UserRoleRequest userRoleRequest)
		{
			var isinrole = await IsUserExistInRole(userRoleRequest);
			if (isinrole)
			{
				return new ErrorRole { ok = false, message =userRoleRequest.user.FirstName+" "+ userRoleRequest.user.LastName + lo[ResourcesKeys.IsExisted]+"In Role" };
			}
			var role = await GetRoleById(userRoleRequest.RoleId);
			if (role is null)
			{
				return new ErrorRole { ok = false, message ="role"+lo[ResourcesKeys.NotFound] };

			}
			var adduserinrole = await userManager.AddToRoleAsync(userRoleRequest.user, role.Name);
			if (!adduserinrole.Succeeded)
			{
				return new ErrorRole { message = string.Join(",", adduserinrole.Errors.ToString()) };
			}
			await userManager.UpdateAsync(userRoleRequest.user);
			return new ErrorRole { ok = true, message = lo[ResourcesKeys.AddedSuccessfully],Role=role };
		}//C done
		public async Task<bool> IsUserExistInRole(UserRoleRequest userRoleRequest)
		{
			var role = await GetRoleById(userRoleRequest.RoleId);
			if (role == null)
			{
				return false;
			}
			var isinrole = await userManager.IsInRoleAsync(userRoleRequest.user, role.Name);
			return isinrole;
		}//q done
		public async Task<ErrorRole> RemoveUserFromRole(UserRoleRequest userRoleRequest)
		{
			var isinrole = await IsUserExistInRole(userRoleRequest);
			var role = await GetRoleById(userRoleRequest.RoleId);
			if (!isinrole || role is null)
			{
				return new ErrorRole { ok = false, message = lo[ResourcesKeys.NotFound]};
			}
			var adduserinrole = await userManager.RemoveFromRoleAsync(userRoleRequest.user, role.Name);
			if (!adduserinrole.Succeeded)
			{
				return new ErrorRole { message = string.Join(",", adduserinrole.Errors.ToString()) };
			}
			await userManager.UpdateAsync(userRoleRequest.user);
			return new ErrorRole { ok = true, message = lo[ResourcesKeys.DeletedSuccessfully],Role=role };
		}//C done

	}
}