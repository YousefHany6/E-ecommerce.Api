using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Users.Commands.Models;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Users.Commands.Handlers
{
	public class ManagerCommandHandler : ResponseHandler,
		                                 IRequestHandler<AddManagerCommandModel, Response<AddManagerModel>>,
		                                 IRequestHandler<EditManagerCommandModel, Response<ResponseEditManager>>
	{
		private readonly IManagerRepo userRepo;
		private readonly IMapper mapper;
		private readonly IStringLocalizer<Resources> lo;

		public ManagerCommandHandler(IManagerRepo userRepo,IMapper mapper, IStringLocalizer<Resources> lo) : base(lo)
		{
			this.userRepo = userRepo;
			this.mapper = mapper;
			this.lo = lo;
		}
		public async Task<Response<AddManagerModel>> Handle(AddManagerCommandModel request, CancellationToken cancellationToken)
		{
			var req = await userRepo.AddManager(request.Model);
			if (req.ok == false)
			{
				return BadRequest<AddManagerModel>(req.Message);
			}
			return Success<AddManagerModel>(null,Message:null);
		}

		public async Task<Response<ResponseEditManager>> Handle(EditManagerCommandModel request, CancellationToken cancellationToken)
		{
			var req = await userRepo.EditManager(request.managerid,request.model);
			if (req.ok == false)
			{
				return BadRequest<ResponseEditManager>(req.Message);
			}
			var managermap = mapper.Map<ResponseEditManager>(req.User);
			return Success<ResponseEditManager>(managermap, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
