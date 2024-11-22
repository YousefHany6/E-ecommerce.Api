using E_ecommerce.Data.DTO.EmailDtoResponse;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_ecommerce.Service.Repo
{
	public class EmailService : IEmailService
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly UserManager<User> userManager;
		private readonly ApplicationContext context;

		public EmailService(
			IStringLocalizer<Resources> lo,
			UserManager<User> userManager,
			ApplicationContext context
			)
		{
			this.lo = lo;
			this.userManager = userManager;
			this.context = context;
		}
		public async Task<ResponseEmailService> SendEmailAsync(string email, string Message,string wellcome= "Wellcome In Web Store")
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new ResponseEmailService
				{
					Success = false,
					Message = email + " " + lo[ResourcesKeys.NotFound],
					data = new SendData
					{
						Email = email,
						EmailMessage = Message
					}
				};
			}
			try
			{
				using (var client = new SmtpClient())
				{
					await client.ConnectAsync("smtp.gmail.com", 587);
					await client.AuthenticateAsync("hanyy4870@gmail.com", "jwstmpknfjxlaoub");
					var bodybuilder = new BodyBuilder()
					{
						HtmlBody = Message,
						TextBody = wellcome
					};
					var message = new MimeMessage()
					{
						Body = bodybuilder.ToMessageBody()
					};
					message.From.Add(new MailboxAddress("Web Store", "hanyy4870@gmail.com"));
					message.To.Add(new MailboxAddress(user.FirstName + " " + user.LastName, email));
					message.Subject = "Wellcome";
					await client.SendAsync(message);
					await client.DisconnectAsync(true);

					return new ResponseEmailService
					{
						Success = true,
						Message = lo[ResourcesKeys.Successfully],
						data = new SendData
						{
							Email = email,
							EmailMessage = Message
						}
					};

				}
			}
			catch (Exception)
			{

				return new ResponseEmailService
				{
					Success = false,
					Message = lo[ResourcesKeys.BadRequest],
					data = new SendData
					{
						Email = email,
						EmailMessage = Message
					}
				};
			}
		}
		public async Task<ResponseEmailService> SendOTPTOEmailAsync(string email)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return new ResponseEmailService
				{
					Success = false,
					Message = lo[ResourcesKeys.NotFound]
				};
			}

			if (await userManager.IsEmailConfirmedAsync(user))
			{
				return new ResponseEmailService
				{
					Success = false,
					Message = "Email is already confirmed"
				};
			}

			// Check if OTP exists and is not expired
			var userEmail = await context.OTPs.FirstOrDefaultAsync(s => s.Email == email && s.ExpirationTime > DateTime.UtcNow);
			string otp;

			if (userEmail == null)
			{
				otp = await GenerateOTP();
				userEmail = new OTP
				{
					OTPCode = otp,
					Email = email,
					ExpirationTime = DateTime.UtcNow.AddMinutes(5).ToLocalTime()
				};
				await context.OTPs.AddAsync(userEmail);
			}
			else
			{
				otp = userEmail.OTPCode;
			}

			await context.SaveChangesAsync();

			var sendOtpResponse = await SendEmailAsync(email, $"OTP to verify your email: {otp}");
			sendOtpResponse.data.EmailMessage = sendOtpResponse.Success ? lo[ResourcesKeys.Send] : lo[ResourcesKeys.BadRequest];

			return sendOtpResponse;
		}
		public async Task<EmailVerfiy> VerfiyEmail(string email, string otp)
		{
			var verfiyotp=await context.OTPs.FirstOrDefaultAsync(p => p.Email == email&&p.OTPCode==otp);
			if (verfiyotp is null || verfiyotp.ExpirationTime<=DateTime.UtcNow||string.IsNullOrEmpty(verfiyotp.OTPCode))
			{
				return new EmailVerfiy { IsVerfiy = false, VerfiyMessage = "Invalid or expired OTP." };
			}
			var user=await userManager.FindByEmailAsync(email);
			user.EmailConfirmed= true;
			await userManager.UpdateAsync(user);
			return new EmailVerfiy { IsVerfiy = true, VerfiyMessage = lo[ResourcesKeys.Successfully] };

		}
		private async Task<string> GenerateOTP(int length = 6)
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				var data= new byte[length];
				rng.GetBytes(data);
				int otp = Math.Abs(BitConverter.ToInt32(data, 0)) % (int)Math.Pow(10, length);

				return otp.ToString($"D{length}");
			}
		}

		
	}
}
