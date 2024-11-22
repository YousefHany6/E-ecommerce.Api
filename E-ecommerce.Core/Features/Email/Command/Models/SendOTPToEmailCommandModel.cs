using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.EmailDtoResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Email.Command.Models
{
	public class SendOTPToEmailCommandModel:IRequest<Response<ResponseEmailService>>
	{
		[Required,EmailAddress]
		public string Email { get; set; }
		public SendOTPToEmailCommandModel(string email)
		{
			Email = email;
		}
	}
}
