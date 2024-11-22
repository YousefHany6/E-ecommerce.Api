using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Product.Command.Model;
using E_ecommerce.Core.Features.Product.Query.Model;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Command.Handler
{
	public class ProductHandlerCommand : ResponseHandler,
																									IRequestHandler<AddProductModel, Response<ProductModelResponse>>,
																									IRequestHandler<EditProductModel, Response<ProductModelResponse>>,
																									IRequestHandler<DeleteProductModel, Response<ProductModelResponse>>
	{
		private readonly IProductService productService;
		private readonly IStringLocalizer<Resources> lo;
		private readonly IMapper mapper;

		public ProductHandlerCommand(
			IProductService productService,
			IStringLocalizer<Resources> lo,
			IMapper mapper) : base(lo)
		{
			this.productService = productService;
			this.lo = lo;
			this.mapper = mapper;
		}

		public async Task<Response<ProductModelResponse>> Handle(AddProductModel request, CancellationToken cancellationToken)
		{
			var req = await productService.AddProduct(request.userID, request.model);
			if (req.Ok==false)
			{
				return NotFound<ProductModelResponse>(req.Message_Error);
			}
			var model = mapper.Map<ProductModelResponse>(req.Product);
			return Success(model, Message: lo[ResourcesKeys.Successfully]);
		}
		public async Task<Response<ProductModelResponse>> Handle(DeleteProductModel request, CancellationToken cancellationToken)
		{
			var req = await productService.DeleteProduct(request.productid);
			if (req.Ok == false)
			{
				return NotFound<ProductModelResponse>();
			}
			var model = mapper.Map<ProductModelResponse>(req.Product);
			return Success(model, Message: lo[ResourcesKeys.DeletedSuccessfully]);
		}

		public async Task<Response<ProductModelResponse>> Handle(EditProductModel request, CancellationToken cancellationToken)
		{
			var req = await productService.EditProduct(request.productid,request.model);
			if (req.Ok == false)
			{
				return NotFound<ProductModelResponse>();
			}
			var model = mapper.Map<ProductModelResponse>(req.Product);
			return Success(model, Message: lo[ResourcesKeys.Successfully]);
		}
	}
}
