using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Discount.Query.Models;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;

namespace E_ecommerce.Core.Features.Discount.Query.Handler
{
	public class DisCountQueryHandler : ResponseHandler,
																																				IRequestHandler<GetAllDiscountsModel, Response<PaginatedResult<DiscountResponse>>>,
																																				IRequestHandler<GetByIdModel, Response<DiscountResponse>>
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly IDiscountRepo discountRepo;
		private readonly IMapper mapper;

		public DisCountQueryHandler(IStringLocalizer<Resources> lo, IDiscountRepo discountRepo,
			IMapper mapper
			) : base(lo)
		{
			this.lo = lo;
			this.discountRepo = discountRepo;
			this.mapper = mapper;
		}
		public async Task<Response<DiscountResponse>> Handle(GetByIdModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.GetById(request.DiscountId);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			var map = mapper.Map<DiscountResponse>(req.Discount);
			return Success(map, Message: req.Message);
		}

		public async Task<Response<PaginatedResult<DiscountResponse>>> Handle(GetAllDiscountsModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.GetAllDiscounts(request.Sereach);
			if (!req.Any())
			{
				return NotFound<PaginatedResult<DiscountResponse>>();
			}
			var map = await mapper.ProjectTo<DiscountResponse>(req).ToPaginatedListAsync(request.PageSize, request.PageNumber);

			return Success(map, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
