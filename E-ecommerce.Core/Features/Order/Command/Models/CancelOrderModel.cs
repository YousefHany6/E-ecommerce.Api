using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.Order.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Command.Models
{
	public class CancelOrderModel : IRequest<Response<OrderResponse>>
	{
		public int Order_Id { get; set; }
		public CancelOrderModel(int order_Id)
		{
			Order_Id = order_Id;
		}
	}
}
