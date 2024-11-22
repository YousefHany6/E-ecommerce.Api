using E_ecommerce.Core.Features.Auth.Queries.Models;
using E_ecommerce.Core.Features.Authorization.Commands.Models;
using E_ecommerce.Core.Features.Authorization.Queries.Models;
using E_ecommerce.Data.Entites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
 [Route("api/V1/[Controller]")]
	[Authorize(Roles ="SuperManager")]
	[ApiController]
	public class AuthorizationRoleController : ControllerBase
	{
		private readonly IMediator mediator;

		public AuthorizationRoleController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpPost("AddRole")]
		public async Task<IActionResult> AddRole([FromBody]AddRoleModel addRoleModel)
		{
			var req = await mediator.Send(addRoleModel);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("AddUserInNewRole")]
		public async Task<IActionResult> AddUserInNewRole([FromBody] AddUserInNewRoleModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteRole/{Role_Id}")]
		public async Task<IActionResult> DeleteRole([FromRoute] int Role_Id)
		{
			var req = await mediator.Send(new DeleteRoleModel(Role_Id));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteUserFromRole")]
		public async Task<IActionResult> DeleteUserFromRole([FromBody]RemoveUserFromRoleModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("EditRole")]
		public async Task<IActionResult> EditRole([FromBody] EditRoleModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("GetRoleById/{id}")]
		public async Task<IActionResult> GetRoleById(int id)
		{
			var req = await mediator.Send(new GetRoleByIdModel(id));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("GetRoleByName/{name:alpha}")]
		public async Task<IActionResult> GetRoleByName(string name)
		{
			var req = await mediator.Send(new GetRoleByNameModel(name));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("GetRoles")]
		public async Task<IActionResult> GetRolesList()
		{
			var req = await mediator.Send(new GetRolesListModel());

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("IsRoleExistById/{roleId}")]
		public async Task<IActionResult>IsRoleExistById(int roleId)
		{
			var req = await mediator.Send(new IsRoleExistByIdModel(roleId));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("IsRoleExistByName/{roleName:alpha}")]
		public async Task<IActionResult> IsRoleExistByName(string roleName)
		{
			var req = await mediator.Send(new IsRoleExistByNameModel(roleName));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("GetRolesFromUser/{User_Id}")]
		public async Task<IActionResult> ManageUserRolesData(int User_Id)
		{
			var req = await mediator.Send(new ManageUserRolesDataModel(User_Id.ToString()));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("IsUserExistInRole")]
		public async Task<IActionResult> IsUserExistInRole([FromBody]IsUserExistInRoleModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
	}
}
