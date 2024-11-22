using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Users.Queries.Models;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.Entites;
using E_ecommerce.Service.Interfaces;
using E_ecommerce.Service.Repo;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Users.Queries.Handlers
{
	public class ManagerQueryHandler :ResponseHandler,
				IRequestHandler<GetUsersRoleManagerModel, PaginatedResult<GetManagersModel>>,
				IRequestHandler<GetManagerById, Response<GetManagersModel>>,
				IRequestHandler<DeleteMangerQueryModel, Response<GetManagersModel>>
	{
		private readonly IMapper mapper;
		private readonly IManagerRepo managerRepo;
		private readonly IStringLocalizer<Resources> lo;

		public ManagerQueryHandler(
			IMapper mapper
			,IManagerRepo _managerRepo
			,IStringLocalizer<Resources>lo
			) : base(lo)
		{
			this.mapper = mapper;
			this.managerRepo = _managerRepo;
			this.lo = lo;
		}

		public  async Task<PaginatedResult<GetManagersModel>> Handle(GetUsersRoleManagerModel request, CancellationToken cancellationToken)
		{
			var req = await  managerRepo.GetUsersWithFilterSearchAndOrderAsync(request.order,request.Search);
		 
			var managers= await mapper.ProjectTo<GetManagersModel>(req).ToPaginatedListAsync(request.PageNumber,request.PageSize);
			if (!string.IsNullOrEmpty(request.Search)&&managers.BaseData.Count==0)
			{
				managers.Message = request.Search + lo[ResourcesKeys.NotFound];
			}
			return managers;	

		}

		public async Task<Response<GetManagersModel>> Handle(GetManagerById request, CancellationToken cancellationToken)
		{
			var req = await managerRepo.GetManagerById(request.Id);
			if (!req.ok)
			{
				return NotFound<GetManagersModel>(req.Message);
			}
			var manger= mapper.Map<GetManagersModel>(req.User);

			return Success(manger, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<GetManagersModel>> Handle(DeleteMangerQueryModel request, CancellationToken cancellationToken)
		{
			var req = await managerRepo.DeleteManger(request.Id);
			if (!req.ok)
			{
				return NotFound<GetManagersModel>(req.Message);
			}
			var manger = mapper.Map<GetManagersModel>(req.User);

			return Deleted<GetManagersModel>(manger);
		}
	}
}
