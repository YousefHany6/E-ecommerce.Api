using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Category.Query.Models;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.CategoryModels;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using E_ecommerce.Service.Repo;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Query.Handler
{
	public class CategoryHandlerQuery : ResponseHandler,
																																		IRequestHandler<CategoryGetByIdModel, Response<ResponseCategoryModel>>,
																																		IRequestHandler<GetALLCategoryWithSerachAndOrder, PaginatedResult<ResponseCategoryModel>>
	{
		private readonly IMapper mapper;
		private readonly ICategoryRepo categoryRepo;
		private readonly IStringLocalizer<Resources> lo;

		public CategoryHandlerQuery(
			IMapper mapper,
			ICategoryRepo categoryRepo,
			IStringLocalizer<Resources> lo) : base(lo)
		{
			this.mapper = mapper;
			this.categoryRepo = categoryRepo;
			this.lo = lo;
		}
		public async Task<Response<ResponseCategoryModel>> Handle(CategoryGetByIdModel request, CancellationToken cancellationToken)
		{
			var req = await categoryRepo.GetByIdCategory(request.categoryId);
			if (req.ok == false)
			{
				return BadRequest<ResponseCategoryModel>(req.Message);
			}
			var result = mapper.Map<ResponseCategoryModel>(req.cat);
			return Success<ResponseCategoryModel>(result, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<PaginatedResult<ResponseCategoryModel>> Handle(GetALLCategoryWithSerachAndOrder request, CancellationToken cancellationToken)
		{
			var req = await categoryRepo.GetCategories(request.order, request.Search);

			var cates = await mapper.ProjectTo<ResponseCategoryModel>(req).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			if (!string.IsNullOrEmpty(request.Search) && cates.BaseData.Count == 0)
			{
				cates.Message = request.Search + lo[ResourcesKeys.NotFound];
			}
			return cates;
		}
	}
}

