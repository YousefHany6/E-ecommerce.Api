using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Query.Models
{
	public class GetByIdModel:IRequest<Response<DiscountResponse>>
	{
		public int DiscountId { get; set; }
		public GetByIdModel(int discountId)
		{
			DiscountId = discountId;
		}
	}
}
