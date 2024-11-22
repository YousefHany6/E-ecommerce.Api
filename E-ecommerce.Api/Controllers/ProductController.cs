using E_ecommerce.Core.Features.Category.Query.Models;
using E_ecommerce.Core.Features.Product.Command.Model;
using E_ecommerce.Core.Features.Product.Query.Model;
using E_ecommerce.Data.DTO.ProductModel.Request;
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
	[Authorize(Roles = "SuperManager")]
	public class ProductController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;

		public ProductController(IMediator mediator,UserManager<User> userManager)
		{
			this.mediator = mediator;
			this.userManager = userManager;
		}
		[HttpGet("GetProducts")]
		public async Task<IActionResult> GetProducts([FromQuery] GetAllProductsmodel model)
		{
			var req = await mediator.Send(model);
			return StatusCode((int)req.StatusCode,req);
		}
		[HttpGet("GetProductById/{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var req = await mediator.Send(new GetProductByIdModel(id));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("AddProduct")]
		public async Task<IActionResult> AddProduct([FromForm]ProductModelRequest model)
		{
			var user = await userManager.GetUserAsync(User);
			var req = await mediator.Send(new AddProductModel(user.Id,model));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("EditProduct/{productid}")]
		public async Task<IActionResult> EditProduct(int productid,[FromForm] EditProductmodelRequest model)
		{
			var req = await mediator.Send(new EditProductModel(model, productid));
			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteProduct/{productid}")]
		public async Task<IActionResult> DeleteProduct(int productid)
		{
			var req = await mediator.Send(new DeleteProductModel(productid));
			return StatusCode((int)req.StatusCode, req);
		}
	}
}
