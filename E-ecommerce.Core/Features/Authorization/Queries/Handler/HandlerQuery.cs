using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Authorization.Commands.Models;
using E_ecommerce.Core.Features.Authorization.Queries.Models;
using E_ecommerce.Data.DTO.AuthorizationRequest;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using E_ecommerce.Data.DTO.Result;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Queries.Handler
{
    public class HandlerQuery : ResponseHandler,
																											IRequestHandler<GetRoleByIdModel,   Response<RoleResponse>>,

																											IRequestHandler<GetRoleByNameModel, Response<RoleResponse>>,

																											IRequestHandler<GetRolesListModel, Response<IEnumerable<RoleResponse>>>,

																											IRequestHandler<IsRoleExistByIdModel,Response<IsExist>>,

																											IRequestHandler<IsRoleExistByNameModel, Response<IsExist>>,

																											IRequestHandler<ManageUserRolesDataModel, Response<ManageUserRoles>>
	{
		private readonly IMapper mapper;
		private readonly IAuthorizationService authorization;
		private readonly UserManager<User> userManager;
		private readonly IStringLocalizer<Resources> lo;

		public HandlerQuery(
			IMapper mapper
			,IAuthorizationService authorization,
			UserManager<User> userManager,
			IStringLocalizer<Resources>lo
			):base(lo)
		{
			this.mapper = mapper;
			this.authorization = authorization;
			this.userManager = userManager;
			this.lo = lo;
		}
		public async Task<Response<RoleResponse>> Handle(GetRoleByIdModel request, CancellationToken cancellationToken)
		{
			var req=await authorization.GetRoleById(request.Id);
			if (req is null)
			{
				return NotFound<RoleResponse>(lo[ResourcesKeys.NotFound]);
			}
			var role=mapper.Map<RoleResponse>(req);
			return Success(role, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<RoleResponse>> Handle(GetRoleByNameModel request, CancellationToken cancellationToken)
		{
			var req = await authorization.GetRoleByName(request.Name);
			if (req is null)
			{
				return NotFound<RoleResponse>();
			}
			var role = mapper.Map<RoleResponse>(req);
			return Success(role, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<IsExist>> Handle(IsRoleExistByIdModel request, CancellationToken cancellationToken)
		{
			var req = await authorization.IsRoleExistById(request.Id);
			if (req is false)
			{
				return NotFound<IsExist>();
			}
			var isexist = new IsExist { Is_Exist = true };
			return Success<IsExist>(isexist, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<IsExist>> Handle(IsRoleExistByNameModel request, CancellationToken cancellationToken)
		{
			var req = await authorization.IsRoleExistByName(request.Name);
			if (req is false)
			{
				return NotFound<IsExist>();
			}
			var isexist = new IsExist { Is_Exist = true };
			return Success<IsExist>(isexist, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<ManageUserRoles>> Handle(ManageUserRolesDataModel request, CancellationToken cancellationToken)
		{
			var user = await userManager.FindByIdAsync(request.User_Id);
			if (user is null)
			{
				return NotFound<ManageUserRoles>();
			}
			var req = await authorization.ManageUserRolesData(user);
			if (!req.Roles.Any())
			{
				return NotFound<ManageUserRoles>();
			}
			return Success(req, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<IEnumerable<RoleResponse>>> Handle(GetRolesListModel request, CancellationToken cancellationToken)
		{
			var req = await authorization.GetRolesList();
			if (!req.Any())
			{
				return NotFound<IEnumerable<RoleResponse>>();
			}
			var roles = mapper.Map<IEnumerable<RoleResponse>>(req);
			return Success(roles, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
