using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.Order.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Query.Models
{
	public class GetAllOrderModel:IRequest<Response<PaginatedResult<AllOrdersResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public OrderStatus? order { get; set; }
	}
}
