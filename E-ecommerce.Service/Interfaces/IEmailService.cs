using E_ecommerce.Data.DTO.EmailDtoResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IEmailService
	{
		Task<ResponseEmailService> SendEmailAsync(string email, string Message, string wellcome = "Wellcome In Web Store");
		Task<ResponseEmailService> SendOTPTOEmailAsync(string email);
		Task<EmailVerfiy>VerfiyEmail(string email,string otp);
	}
}
