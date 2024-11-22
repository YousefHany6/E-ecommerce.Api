using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Auth.Commands.Models;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Auth.Commands.Handlers
{
	public class AuthCommandHandlers : ResponseHandler,
		IRequestHandler<RegisterUserCommand, Response<AuthModel>>       ,
		IRequestHandler<LoginQueryModel, Response<AuthModel>>


	{
		private readonly IAuth auth;
		private readonly IStringLocalizer<Resources> lo;

		public AuthCommandHandlers(IAuth auth,IStringLocalizer<Resources>lo) : base(lo)
		{
			this.auth = auth;
			this.lo = lo;
		}
		public async Task<Response<AuthModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var req = await auth.Register(request._register);
			if (req.IsAuthenticated==false)
			{
				return BadRequest<AuthModel>(req.Message);
			}
			return Success<AuthModel>(req);
		}
		public async Task<Response<AuthModel>> Handle(LoginQueryModel request, CancellationToken cancellationToken)
		{
			var req = await auth.Login(request._login);

			if (req.IsAuthenticated == false)
			{
				return BadRequest<AuthModel>(req.Message);
			}
			return Success<AuthModel>(req, Message: "Login Successfully");
		}
	}
}
