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
	public class AddManagerModel
	{
		[Required, MaxLength(60)]
		public string FirstName { get; set; }
		[Required, MaxLength(60)]
		public string LastName { get; set; }
		public IFormFile? Image { get; set; }
		[Required]
		public Gender Gender { get; set; }
		[Required]
		public List<string> PhoneNumbers { get; set; }
		public List<string>? Addresses { get; set; }

		[Required, MinLength(8)]
		public string Password { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
	}
}
