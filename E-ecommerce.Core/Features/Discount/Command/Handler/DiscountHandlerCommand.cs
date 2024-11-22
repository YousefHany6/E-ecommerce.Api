using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Discount.Command.Models;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Command.Handler
{
	public class DiscountHandlerCommand : ResponseHandler,
																																					IRequestHandler<AddDiscountModel, Response<DiscountResponse>>,
																																					IRequestHandler<EditDiscountModel, Response<DiscountResponse>>,
																																					IRequestHandler<DeleteDiscountModel, Response<DiscountResponse>>,
																																					IRequestHandler<ApplyDiscountOnAllProductModel, Response<DiscountResponse>>,
																																					IRequestHandler<ApplyDiscountOnOneProductModel, Response<DiscountResponse>>,
																																					IRequestHandler<ApplyDiscountOnSpecificCategoryModel, Response<DiscountResponse>>,
			                                 	IRequestHandler<DeleteDiscountOnAllProductModel, Response<DiscountResponse>>,
																																					IRequestHandler<DeleteDiscountOnOneProductModel, Response<DiscountResponse>>,
																																					IRequestHandler<DeleteDiscountOnSpecificCategoryModel, Response<DiscountResponse>>
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly IDiscountRepo discountRepo;
		private readonly IMapper mapper;

		public DiscountHandlerCommand(IStringLocalizer<Resources> lo, IDiscountRepo discountRepo,
			IMapper mapper
			) : base(lo)
		{
			this.lo = lo;
			this.discountRepo = discountRepo;
			this.mapper = mapper;
		}

		public async Task<Response<DiscountResponse>> Handle(AddDiscountModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.AddDiscount(request.add);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			var map = mapper.Map<DiscountResponse>(req.Discount);
			return Success(map, Message: req.Message);
		}

		public async Task<Response<DiscountResponse>> Handle(EditDiscountModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.EditDiscount(request.DiscountId, request.Model);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			var map = mapper.Map<DiscountResponse>(req.Discount);
			return Success(map, Message: req.Message);
		}

		public async Task<Response<DiscountResponse>> Handle(DeleteDiscountModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.Delete(request.Discountid);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			var map = mapper.Map<DiscountResponse>(req.Discount);
			return Success(map, Message: req.Message);
		}

		public async Task<Response<DiscountResponse>> Handle(ApplyDiscountOnAllProductModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.ApplyDiscountOnAllProduct(request.Discountid, request.DiscountExpire);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<DiscountResponse>> Handle(ApplyDiscountOnOneProductModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.ApplyDiscountOnOneProduct(request.DiscountId, request.ProductId, request.DiscountExpire);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<DiscountResponse>> Handle(ApplyDiscountOnSpecificCategoryModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.ApplyDiscountOnSpecificCategory(request.DiscountId, request.CategoryId, request.DiscountExpire);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<DiscountResponse>> Handle(DeleteDiscountOnAllProductModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.DeleteDiscountOnAllProduct();
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<DiscountResponse>> Handle(DeleteDiscountOnOneProductModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.DeleteDiscountOnOneProduct(request.ProductID);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<DiscountResponse>> Handle(DeleteDiscountOnSpecificCategoryModel request, CancellationToken cancellationToken)
		{
			var req = await discountRepo.DeleteDiscountOnSpecificCategory(request.CategoryID);
			if (req.ok == false)
			{
				return BadRequest<DiscountResponse>(req.Message);
			}
			return Success<DiscountResponse>(null, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
