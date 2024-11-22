using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Category.Command.Models;
using E_ecommerce.Data.DTO.CategoryModels;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Command.Handler
{
	public class CategoryCommandHandler:ResponseHandler,
		                                IRequestHandler<Add_CategoryModel, Response<ResponseCategoryModel>>,
		                                IRequestHandler<Edit_CategoryModel, Response<ResponseCategoryModel>>,
		                                IRequestHandler<Delete_CategoryModel, Response<ResponseCategoryModel>>
	{
		private readonly IMapper mapper;
		private readonly ICategoryRepo categoryRepo;
		private readonly IStringLocalizer<Resources> lo;

		public CategoryCommandHandler(
			IMapper mapper,
			ICategoryRepo categoryRepo,
			IStringLocalizer<Resources> lo) : base(lo)
		{
			this.mapper = mapper;
			this.categoryRepo = categoryRepo;
			this.lo = lo;
		}

		public async Task<Response<ResponseCategoryModel>> Handle(Edit_CategoryModel request, CancellationToken cancellationToken)
		{
			var req = await categoryRepo.EditCategory(request.CategoryID,request.model);
			if (req.ok == false)
			{
				return BadRequest<ResponseCategoryModel>(req.Message);
			}
			var result = mapper.Map<ResponseCategoryModel>(req.cat);
			return Success(result, Message: lo[ResourcesKeys.Successfully]);
		}

		public async Task<Response<ResponseCategoryModel>> Handle(Delete_CategoryModel request, CancellationToken cancellationToken)
		{
			var req = await categoryRepo.DeleteCategory(request.CategoryId);
			if (req.ok == false)
			{
				return BadRequest<ResponseCategoryModel>(req.Message);
			}
			var result = mapper.Map<ResponseCategoryModel>(req.cat);
			return Deleted(result);
		}

		public async Task<Response<ResponseCategoryModel>> Handle(Add_CategoryModel request, CancellationToken cancellationToken)
		{
			var req = await categoryRepo.AddCategory(
				new AddCategoryModel
				{
					Name = request.Name,
					Description = request.Description,
					ImageFile= request.ImageFile,
				}
				);
			if (req.ok==false)
			{
				return BadRequest<ResponseCategoryModel>(req.Message);
			}
			var result=mapper.Map<ResponseCategoryModel>(req.cat);
			return Success(result);
		}
	}
}
