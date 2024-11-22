using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.AuthorizationRequest
{
	public class UserRoleRequest
	{
		[Required]
		public int RoleId { get; set; }
		[Required]
		public User user { get; set; }
	}
}
