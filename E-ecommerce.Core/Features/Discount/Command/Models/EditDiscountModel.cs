using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.DiscountDto.Request;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Command.Models
{
	public class EditDiscountModel : IRequest<Response<DiscountResponse>>
	{
		public EditDiscountRequest Model { get; set; }
		public int DiscountId { get; set; }
		public EditDiscountModel(EditDiscountRequest model,int discountid)
		{
			Model = model;
			DiscountId = discountid;
		}
	}
}
