using E_ecommerce.Core.Features.Email.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "SuperManager,Manager")]

	public class EmailController : ControllerBase
	{
		private readonly IMediator mediator;

		public EmailController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpPost("SendMessage")]
		public async Task<IActionResult> SendMessage(SendEmailCommandModel model)
		{
			var req=await mediator.Send(model);
			return StatusCode((int)req.StatusCode, req);
		}

		
	}
}
