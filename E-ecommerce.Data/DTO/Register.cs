using E_ecommerce.Data.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO
{
	public class Register
	{
		[Required,MaxLength(50)]
		public string  FName { get; set; }
		[Required, MaxLength(50)]
		public string LName { get; set; }
		[Required,EmailAddress]
		public string Email { get; set; }
		[Required,MinLength(8)]
		public string Password { get; set; }
		[Required, MinLength(8),Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		public IFormFile? Photo { get;set; }
		[Required]
		public Gender Gender { get; set; }
	}
}
