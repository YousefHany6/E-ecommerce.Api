using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.Order.Request;
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
	public class AddProductInOrderModel : IRequest<Response<OrderResponse>>
	{
		[Required]
		public int Order_Id { get; set; }
		[Required]
		public OrderProduct Products { get; set; }
		public AddProductInOrderModel(int orderid,OrderProduct products)
		{
			Order_Id = orderid;
			Products = products;
		}

		
	}
}
