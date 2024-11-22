using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Authorization.Commands.Models;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using E_ecommerce.Data.DTO.Result;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Net;

namespace E_ecommerce.Core.Features.Authorization.Commands.Handler
{
	public class HandlerCommand : ResponseHandler,
																								IRequestHandler<AddRoleModel,Response<RoleResponse>>,
																								IRequestHandler<DeleteRoleModel,Response<RoleResponse>>,
																								IRequestHandler<EditRoleModel,Response<RoleResponse>>,
																								IRequestHandler<AddUserInNewRoleModel, Response<RoleResponse>>,
																								IRequestHandler<RemoveUserFromRoleModel, Response<RoleResponse>>,                
		                      IRequestHandler<IsUserExistInRoleModel, Response<IsExist>>

	{
		private readonly IMapper mapper;
		private readonly IAuthorizationService authorizationService;
		private readonly IStringLocalizer<Resources> lo;
		private readonly UserManager<User> userManager;

		public HandlerCommand(
			IMapper mapper,
			IAuthorizationService authorizationService,
			IStringLocalizer<Resources> lo,
			UserManager<User> userManager
			) : base(lo)
		{
			this.mapper = mapper;
			this.authorizationService = authorizationService;
			this.lo = lo;
			this.userManager = userManager;
		}
		public async Task<Response<RoleResponse>> Handle(AddRoleModel request, CancellationToken cancellationToken)
		{
			var req = await authorizationService.AddRoleAsync(request.Role_Name);
			if (!req.ok)
			{
				return BadRequest<RoleResponse>(req.message);
			}
			var role = mapper.Map<RoleResponse>(req.Role);
			return  Success<RoleResponse>(role);
		}
		public async Task<Response<RoleResponse>> Handle(DeleteRoleModel request, CancellationToken cancellationToken)
		{
			var req = await authorizationService.DeleteRoleAsync(request.Role_Id);
			if (!req.ok)
			{
				return BadRequest<RoleResponse>(req.message);
			}
			var role = mapper.Map<RoleResponse>(req.Role);
			return Success<RoleResponse>(role, Message: lo[ResourcesKeys.DeletedSuccessfully]);
		}
		public async Task<Response<RoleResponse>> Handle(EditRoleModel request, CancellationToken cancellationToken)
		{
			var req = await authorizationService.EditRoleAsync(request.model);
			if (!req.ok)
			{
				return BadRequest<RoleResponse>(req.message);
			}
			var role = mapper.Map<RoleResponse>(req.Role);
			return Success<RoleResponse>(role, Message: lo[ResourcesKeys.Successfully]);
		}
		public async Task<Response<RoleResponse>> Handle(AddUserInNewRoleModel request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByIdAsync(request.User_Id.ToString());
			if (user is null)
			{
				return NotFound<RoleResponse>();
			}
			var req = await authorizationService.AddUserInNewRole(new Data.DTO.AuthorizationRequest.UserRoleRequest 
			                                                      {RoleId=request.Role_Id,user=user });
			if (!req.ok)
			{
				return BadRequest<RoleResponse>(req.message);
			}
			var role=mapper.Map<RoleResponse>(req.Role);
			return Success(role);
		}
		public async Task<Response<RoleResponse>> Handle(RemoveUserFromRoleModel request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByIdAsync(request.User_Id.ToString());
			if (user is null)
			{
				return NotFound<RoleResponse>();
			}
			var req = await authorizationService.RemoveUserFromRole(new Data.DTO.AuthorizationRequest.UserRoleRequest
			{ RoleId = request.Role_Id, user = user });
			if (!req.ok)
			{
				return BadRequest<RoleResponse>(req.message);
			}
			var role = mapper.Map<RoleResponse>(req.Role);
			return Success(role, lo[ResourcesKeys.Successfully]);
		}
		public async Task<Response<IsExist>> Handle(IsUserExistInRoleModel request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByIdAsync(request.User_Id.ToString());
			if (user is null)
			{
				return NotFound<IsExist>();
			}
			var req = await authorizationService.IsUserExistInRole(new Data.DTO.AuthorizationRequest.UserRoleRequest()
			{ RoleId = request.Role_Id, user = user });
			if (req is false)
			{
				return NotFound<IsExist>();
			}
			var isexist = new IsExist { Is_Exist = true };
			return Success<IsExist>(isexist, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
