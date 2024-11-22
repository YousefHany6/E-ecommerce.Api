using E_ecommerce.Core.Features.Users.Commands.Models;
using E_ecommerce.Core.Features.Users.Queries.Models;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.UserModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	[Authorize(Roles = "SuperManager")]
	public class ManagerController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ManagerController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("GetManagers")]
		public async Task<IActionResult> GetManagers([FromQuery] GetUsersRoleManagerModel req)
		{

			var Request = await _mediator.Send(req);
			return Ok(Request);
		}
		[HttpPost("AddManager")]
		public async Task<IActionResult> AddManager([FromForm] AddManagerModel model)
		{
			var Request = await _mediator.Send(new AddManagerCommandModel(model));
			return StatusCode((int)Request.StatusCode, Request);
		}
		[HttpPut("EditManager/{id}")]
		public async Task<IActionResult> EditManager([FromRoute] int id, [FromForm] EditManagerModel model)
		{
			var Request = await _mediator.Send(new EditManagerCommandModel(id, model));
			return StatusCode((int)Request.StatusCode, Request);
		}

		[HttpGet("GetUserById/{id}")]
		public async Task<IActionResult> GetManagerById([FromRoute] int id)
		{
			var req = await _mediator.Send(new GetManagerById(id));
			return StatusCode((int)req.StatusCode, req);
		}

		[HttpDelete("DeleteManager/{id}")]
		public async Task<IActionResult> DeleteManager([FromRoute] int id)
		{
			var req = await _mediator.Send(new DeleteMangerQueryModel(id));
			return StatusCode((int)req.StatusCode, req);
		}
	}
}
