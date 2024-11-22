using E_ecommerce.Core.Features.Category.Command.Models;
using E_ecommerce.Core.Features.Category.Query.Models;
using E_ecommerce.Data.DTO.CategoryModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="SuperManager")]
	public class CategoryController : ControllerBase
	{
		private readonly IMediator mediator;

		public CategoryController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAll([FromQuery] GetALLCategoryWithSerachAndOrder model)
		{
			var req = await mediator.Send(model);
			return req.BaseData.Any()? Ok(req) : NotFound(req);
		}
		[HttpGet("GetCategoryById/{id}")]
		public async Task<IActionResult> GetById([FromRoute] int id)
		{
			var req = await mediator.Send(new CategoryGetByIdModel(id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("AddCategory")]
		public async Task<IActionResult> Add([FromForm] Add_CategoryModel model)
		{
			var req = await mediator.Send(model);
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("EditCategory{id}")]
		public async Task<IActionResult> Edit([FromRoute]int id,[FromForm] EditCategoryModel model)
		{
			var req = await mediator.Send(new Edit_CategoryModel(id,model));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteCategory/{id}")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var req = await mediator.Send(new Delete_CategoryModel(id));
			return StatusCode((int)req.StatusCode, req);
		}
	}
}
