using E_ecommerce.Core.Bases;
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
	public class DeleteOrdersModel : IRequest<Response<OrderResponse>>
	{
		[Required]
		public List<int> Orders_ID { get; set; }
		public DeleteOrdersModel(List<int> orders_ID)
		{
			Orders_ID = orders_ID;
		}
	}
}
