using E_ecommerce.Core.Bases;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.Order.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Command.Models
{
	public class ChangeStatusOFOrderModel : IRequest<Response<OrderResponse>>
	{
		[Required]
		public List<int>? Orders_Id { get; set; }
		[Required]
		public OrderStatus	OrderStatus { get; set; }

	}
}
