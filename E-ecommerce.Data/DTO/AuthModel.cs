using E_ecommerce.Data.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO
{
	public class AuthModel
	{
		public string? Message { get; set; }
		public bool IsAuthenticated { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
		public string? Role { get; set; }
		public string? Gender { get; set; }
		public string? Token { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime? ExpiresOn { get; set; }

		[JsonIgnore]
		public string? RefreshToken { get; set; }

		public DateTime RefreshTokenExpiration { get; set; }
	}
}
