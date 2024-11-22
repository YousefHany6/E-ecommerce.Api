using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.AuthorizationRequest
{
	public class ErrorRole
	{
		public bool ok { get; set; }
		public string? message { get; set; }
		public  Role Role { get; set; }
	}
}
