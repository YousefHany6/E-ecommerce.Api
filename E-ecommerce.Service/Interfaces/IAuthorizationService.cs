using E_ecommerce.Data.DTO.AuthorizationRequest;
using E_ecommerce.Data.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IAuthorizationService
	{
		Task<Role> GetRoleByName(string Name);
		Task<ErrorRole> AddRoleAsync(string roleName);
		Task<bool> IsRoleExistByName(string roleName);
		Task<ErrorRole> EditRoleAsync(EditRoleRequest request);
		Task<ErrorRole> DeleteRoleAsync(int roleId);
		Task<bool> IsRoleExistById(int roleId);
		Task<IEnumerable<Role>> GetRolesList();
		Task<Role> GetRoleById(int id);
		Task<ManageUserRoles> ManageUserRolesData(User user);
		Task<bool> IsUserExistInRole(UserRoleRequest userRoleRequest);
		Task<ErrorRole> AddUserInNewRole(UserRoleRequest userRoleRequest);
		Task<ErrorRole> RemoveUserFromRole(UserRoleRequest userRoleRequest);
	}
}
