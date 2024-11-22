using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Query.Models
{
	public class GetAllDiscountsModel:IRequest<Response<PaginatedResult<DiscountResponse>>>
	{
		public string? Sereach { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }

	}
}
