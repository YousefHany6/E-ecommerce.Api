using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.Order.Request;
using E_ecommerce.Data.DTO.Order.Response;
using E_ecommerce.Data.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Command.Models
{
	public class AddOrderModel:IRequest<Response<OrderResponse>>
	{
		[Required]
		public OrderRequest order  { get; set; }
		
		public int userid { get; set; }
		public AddOrderModel(OrderRequest order,int userid)
		{
			this.order = order;
			this.userid = userid;
		}
	}
}
