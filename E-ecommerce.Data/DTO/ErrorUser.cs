using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO
{
	public class ErrorUser
	{
		public string? Message { get; set; }
		public bool ok { get; set; }

		public User? User { get; set; }
	}
}
