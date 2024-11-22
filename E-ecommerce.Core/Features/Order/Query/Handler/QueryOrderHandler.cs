using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Order.Query.Models;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.Order.Response;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Query.Handler
{
	public class QueryOrderHandler : ResponseHandler,
																									IRequestHandler<GetAllOrderModel, Response<PaginatedResult<AllOrdersResponse>>>
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly IOrderRepo orderRepo;
		private readonly IMapper mapper;

		public QueryOrderHandler(IStringLocalizer<Resources> lo,
			IOrderRepo orderRepo,
			IMapper mapper) : base(lo)
		{
			this.lo = lo;
			this.orderRepo = orderRepo;
			this.mapper = mapper;
		}

		public async Task<Response<PaginatedResult<AllOrdersResponse>>> Handle(GetAllOrderModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.GetAllOrders(request.order);
			if (!req.Any())
			{
				return NotFound<PaginatedResult<AllOrdersResponse>>("Orders"+" "+ lo[ResourcesKeys.NotFound]);
			}
			var map=await mapper.ProjectTo<AllOrdersResponse>(req).ToPaginatedListAsync(request.PageNumber,request.PageSize);
			return Success(map, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
