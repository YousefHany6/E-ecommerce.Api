using E_ecommerce.Core.Features.Order.Command.Models;
using E_ecommerce.Core.Features.Order.Query.Models;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.Order.Request;
using E_ecommerce.Data.Entites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class OrderController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;

		public OrderController(IMediator mediator,UserManager<User> userManager)
		{
			this.mediator = mediator;
			this.userManager = userManager;
		}
		[Authorize(Roles = "Manager,SuperManager")]
		[HttpGet("GetAllOrders")]
		public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrderModel model)
		{
			var req=await mediator.Send(model);
			return StatusCode((int)req.StatusCode, req);
		}
		[Authorize(Roles = "Manager,SuperManager")]
		[HttpGet("GetOrderByID/{Order_Id}")]
		public async Task<IActionResult> GetOrderByID([FromRoute] int Order_Id)
		{
			var req = await mediator.Send(new GetOrderByIDModel(Order_Id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("AddOrder")]
		[Authorize]
		public async Task<IActionResult> AddOrder(
		[FromBody]OrderRequest model)
		{
			var customer = await userManager.GetUserAsync(User);
			var req = await mediator.Send(new AddOrderModel(model,customer.Id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("AddProductInOrder/{Order_Id}")]
		[Authorize]
		public async Task<IActionResult> AddProductInOrder([FromRoute]int Order_Id,[FromBody] OrderProduct model)
		{
			var req = await mediator.Send(new AddProductInOrderModel(Order_Id,model));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("CancelOrder/{Order_Id}")]
		[Authorize]
		public async Task<IActionResult> CancelOrder([FromRoute] int Order_Id)
		{
			var req = await mediator.Send(new CancelOrderModel(Order_Id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("ChangeStatusOFOrder")]
		[Authorize(Roles = "Manager,SuperManager")]
		public async Task<IActionResult> ChangeStatusOFOrder([FromBody] ChangeStatusOFOrderModel model)
		{
			var req = await mediator.Send(model);
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteProductFromOrder/{Order_Id}")]
		[Authorize]
		public async Task<IActionResult> DeleteProductFromOrder(int Order_Id,[FromBody] List<int> Products_Id)
		{
			var req = await mediator.Send(new DeleteProductFromOrderModel(Products_Id,Order_Id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteOrders")]
		[Authorize(Roles = "Manager,SuperManager")]
		public async Task<IActionResult> Delete([FromBody]DeleteOrdersModel model)
		{
			var req = await mediator.Send(model);
			return StatusCode((int)req.StatusCode, req);
		}

	}
}
