using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class OTP
	{
		[Key]
		public int ID { get; set; }
		[Required,EmailAddress]
		public string Email { get; set; }
		[Required]
		public string OTPCode { get; set; }
		public DateTime ExpirationTime { get; set; }=DateTime.UtcNow.AddMinutes(5).ToLocalTime();
		public DateTime createTime { get; set; }=DateTime.UtcNow.ToLocalTime();
	}
}
