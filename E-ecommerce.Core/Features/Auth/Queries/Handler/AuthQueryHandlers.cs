using Azure.Core;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Auth.Commands.Models;
using E_ecommerce.Core.Features.Auth.Queries.Models;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.Auth;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Auth.Queries.Handler
{
    public class AuthQueryHandlers : ResponseHandler,
		                              IRequestHandler<RefershTokenModel,Response<AuthModel>>,
		                              IRequestHandler<IsEmailConfirmModel,Response<IsEmailConfirmResponse>>
	{
		private readonly IAuth auth;
		private readonly IStringLocalizer<Resources> lo;
		private readonly UserManager<User> userManager;

		public AuthQueryHandlers(IAuth auth,
			IStringLocalizer<Resources>lo,
			UserManager<User> userManager) : base(lo)
		{
			this.auth = auth;
			this.lo = lo;
			this.userManager = userManager;
		}
		

		public async Task<Response<AuthModel>> Handle(RefershTokenModel request, CancellationToken cancellationToken)
		{
			var req = await auth.RefreshTokenAsync(request._refreshtoken);
			if (req.IsAuthenticated == false)
			{
				return BadRequest<AuthModel>(req.Message);
			}
			return Success<AuthModel>(req,Message:"Success");
		}

		public async Task<Response<IsEmailConfirmResponse>> Handle(IsEmailConfirmModel request, CancellationToken cancellationToken)
		{
			var user=await userManager.FindByIdAsync(request.UserId.ToString());
			if (user is null)
			{
				return NotFound<IsEmailConfirmResponse>(lo[ResourcesKeys.NotFound]);
			}
			var isconfirm=await userManager.IsEmailConfirmedAsync(user);
		return Success(new IsEmailConfirmResponse { ISConfirm = isconfirm }, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
