using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.EmailDtoResponse
{
	public class OtpDto
	{
		[Required]
		public string OTP { get; set; }
	}
}
