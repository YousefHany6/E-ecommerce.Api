using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.AuthorizationRequest
{
	public class ManageUserRoles
	{
		public int UserId { get; set; }
		public List<UserRole>? Roles { get; set; }
	}
	public class UserRole
	{
		public int RoleId { get; set; }
		public string RoleName { get; set; }
	}

}
