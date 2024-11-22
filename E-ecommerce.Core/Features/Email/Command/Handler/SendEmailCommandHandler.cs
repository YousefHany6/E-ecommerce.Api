using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Email.Command.Models;
using E_ecommerce.Data.DTO.EmailDtoResponse;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Email.Command.Handler
{
	public class SendEmailCommandHandler : ResponseHandler,
																																						IRequestHandler<SendEmailCommandModel, Response<ResponseEmailService>>,
																																						IRequestHandler<VerfiyEmailCommandModel, Response<EmailVerfiy>>,
																																						IRequestHandler<SendOTPToEmailCommandModel, Response<ResponseEmailService>>
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly IEmailService emailService;

		public SendEmailCommandHandler(IStringLocalizer<Resources>lo,IEmailService emailService):base(lo) 
		{
			this.lo = lo;
			this.emailService = emailService;
		}
		public async Task<Response<ResponseEmailService>> Handle(SendEmailCommandModel request, CancellationToken cancellationToken)
		{
			var req = await emailService.SendEmailAsync(request.Email, request.Message);
			if (!req.Success)
			{
				return BadRequest<ResponseEmailService>(req.Message);
			}
			return Success(req, Message: lo[ResourcesKeys.Successfully]);
		}
		public async Task<Response<EmailVerfiy>> Handle(VerfiyEmailCommandModel request, CancellationToken cancellationToken)
		{
			var req = await emailService.VerfiyEmail(request.Email, request.OTP);
			if (!req.IsVerfiy)
			{
				return BadRequest<EmailVerfiy>();
			}
			return Success(req, Message: lo[ResourcesKeys.Successfully]);
		}
		public async Task<Response<ResponseEmailService>> Handle(SendOTPToEmailCommandModel request, CancellationToken cancellationToken)
		{
			var req = await emailService.SendOTPTOEmailAsync(request.Email);
			if (!req.Success)
			{
				return BadRequest<ResponseEmailService>(req.Message);
			}
			return Success(req, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
