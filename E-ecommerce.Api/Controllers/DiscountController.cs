using E_ecommerce.Core.Features.Discount.Command.Models;
using E_ecommerce.Core.Features.Discount.Query.Models;
using E_ecommerce.Data.DTO.DiscountDto.Request;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_ecommerce.Api.Controllers
{
	[Route("api/V1/[controller]")]
	[ApiController]
	public class DiscountController : ControllerBase
	{
		private readonly IMediator mediator;

		public DiscountController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet("GetAllDiscounts")]
		public async Task<IActionResult> GetAllDiscounts([FromQuery]GetAllDiscountsModel model)
		{
			var req=await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpGet("GetById/{DiscountId}")]
		public async Task<IActionResult> GetById([FromRoute] int DiscountId)
		{
			var req = await mediator.Send(new GetByIdModel(DiscountId));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("AddDiscount")]
		public async Task<IActionResult> AddDiscount([FromBody]AddDiscoundRequest model)
		{
			var req = await mediator.Send(new AddDiscountModel(model));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("ApplyDiscountOnAllProduct")]
		public async Task<IActionResult> ApplyDiscountOnAllProduct([FromForm] ApplyDiscountOnAllProductModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("ApplyDiscountOnOneProduct")]
		public async Task<IActionResult> ApplyDiscountOnOneProduct([FromForm] ApplyDiscountOnOneProductModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPost("ApplyDiscountOnSpecificCategory")]
		public async Task<IActionResult> ApplyDiscountOnSpecificCategory([FromForm] ApplyDiscountOnSpecificCategoryModel model)
		{
			var req = await mediator.Send(model);

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpPut("EditDiscount/{DiscountId}")]
		public async Task<IActionResult> EditDiscount([FromRoute] int DiscountId, [FromBody] EditDiscountRequest model)
		{
			var req = await mediator.Send(new EditDiscountModel(model, DiscountId));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteDiscount/{DiscountId}")]
		public async Task<IActionResult> Delete([FromRoute] int DiscountId)
		{
			var req = await mediator.Send(new DeleteDiscountModel(DiscountId));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteDiscountOnAllProduct")]
		public async Task<IActionResult> DeleteDiscountOnAllProduct()
		{
			var req = await mediator.Send(new DeleteDiscountOnAllProductModel());

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteDiscountOnOneProduct/{ProductID}")]
		public async Task<IActionResult> DeleteDiscountOnOneProduct([FromRoute] int ProductID)
		{
			var req = await mediator.Send(new DeleteDiscountOnOneProductModel(ProductID));

			return StatusCode((int)req.StatusCode, req);
		}
		[HttpDelete("DeleteDiscountOnSpecificCategory/{CategoryID}")]
		public async Task<IActionResult> DeleteDiscountOnSpecificCategory([FromRoute] int CategoryID)
		{
			var req = await mediator.Send(new DeleteDiscountOnSpecificCategoryModel(CategoryID));

			return StatusCode((int)req.StatusCode, req);
		}

	}
}
