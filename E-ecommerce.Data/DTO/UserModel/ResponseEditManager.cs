using E_ecommerce.Data.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.UserModel
{
	public class ResponseEditManager
	{
		public string ManagerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Image { get; set; }
		public Gender Gender { get; set; }
		public List<string> PhoneNumbers { get; set; }
		public List<string>? Addresses { get; set; }
		public string Email { get; set; }
	}
}
