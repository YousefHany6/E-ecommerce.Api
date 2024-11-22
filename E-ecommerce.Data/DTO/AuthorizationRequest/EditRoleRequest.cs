using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.AuthorizationRequest
{
	public class EditRoleRequest
	{
		[Required]
		public int old_Role_ID { get; set; }
		[Required]
		public string New_Role_Name { get; set; }
	}
}
