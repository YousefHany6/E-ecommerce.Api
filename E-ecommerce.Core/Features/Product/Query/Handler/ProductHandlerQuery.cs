
using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Product.Query.Model;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using Elfie.Serialization;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Query.Handler
{
	public class ProductHandlerQuery : ResponseHandler,
																												IRequestHandler<GetAllProductsmodel, Response<PaginatedResult<ProductModelResponse>>>,
																												IRequestHandler<GetProductByIdModel, Response<ProductModelResponse>>
	{
		private readonly IProductService productService;
		private readonly IStringLocalizer<Resources> lo;
		private readonly IMapper mapper;

		public ProductHandlerQuery(
			IProductService productService,
			IStringLocalizer<Resources>lo,
			IMapper mapper):base(lo)
		{
			this.productService = productService;
			this.lo = lo;
			this.mapper = mapper;
		}
		public async Task<Response<PaginatedResult<ProductModelResponse>>> Handle(GetAllProductsmodel request, CancellationToken cancellationToken)
		{
			var req = await productService.GetAllProducts(request.order,request.Search);
			if (!req.Any())
			{
				return NotFound<PaginatedResult<ProductModelResponse>>();
			}
			var model=await mapper.ProjectTo<ProductModelResponse>(req).ToPaginatedListAsync(request.PageNumber,request.PageSize);
			return Success(model, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<ProductModelResponse>> Handle(GetProductByIdModel request, CancellationToken cancellationToken)
		{
			var req = await productService.GetProductById(request.ID);
			if (req.Ok==false)
			{
				return BadRequest<ProductModelResponse>(req.Message_Error);
			}
			var model = mapper.Map<ProductModelResponse>(req.Product);
			return Success(model,Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
