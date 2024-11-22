using E_ecommerce.Core.Features.Auth.Commands.Models;
using E_ecommerce.Core.Features.Auth.Queries.Models;
using E_ecommerce.Core.Features.Email.Command.Models;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.EmailDtoResponse;
using E_ecommerce.Data.Entites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;

		public AuthController(IMediator mediator,UserManager<User> userManager)
		{
			this.mediator = mediator;
			this.userManager = userManager;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> RegisterAsync([FromForm]Register register)
		{
			var Request = await mediator.Send(new RegisterUserCommand(register));
			if (Request.Data is not null&&!string.IsNullOrEmpty(Request.Data.RefreshToken))
				await SetRefreshTokenInCookie(Request.Data.RefreshToken, Request.Data.RefreshTokenExpiration); 
			return StatusCode((int)Request.StatusCode,Request);
		}
		[HttpPost("Login")]
		public async Task<IActionResult> LoginAsync([FromBody] Login login)
		{
			var Request = await mediator.Send(new LoginQueryModel(login));

			if (Request.Data != null&&!string.IsNullOrEmpty(Request.Data.RefreshToken))
			{
			await	SetRefreshTokenInCookie(Request.Data.RefreshToken, Request.Data.RefreshTokenExpiration);

			}
			return StatusCode((int)Request.StatusCode, Request);
		}
		[HttpGet("refreshToken")]
		public async Task<IActionResult> RefreshToken()
		{
			var refreshtoken = Request.Cookies["refreshToken"];
			var req = await mediator.Send(new RefershTokenModel(refreshtoken));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("IsEmailConfirm")]
		[Authorize]
		public async Task<IActionResult> IsEmailConfirm()
		{
			var user = await userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound("User Not Found");
			}
			var req = await mediator.Send(new IsEmailConfirmModel(user.Id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("ConFirmEmail")]
		[Authorize]
		public async Task<IActionResult> ConfirmEmail([FromBody] OtpDto OTP)
		{
			var user = await userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound("User Not Found");
			}
			var req = await mediator.Send(new VerfiyEmailCommandModel(user.Email,OTP.OTP));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("SendCodeTOVerfiyEmail")]
		[Authorize]
		public async Task<IActionResult> Sendcode()
		{
			var user = await userManager.GetUserAsync(User);
			if (user is null)
			{
				return NotFound("User Not Found");
			}
			var req = await mediator.Send(new SendOTPToEmailCommandModel(user.Email));
			return StatusCode((int)req.StatusCode, req);
		}
		private async Task SetRefreshTokenInCookie(string refreshToken, DateTime expires)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = expires.ToLocalTime(),
				Secure = true,
				IsEssential = true,
				SameSite = SameSiteMode.None
			};

			 Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
		}
	}
}
