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
	public class GetOrderByIDModel : IRequest<Response<OrderResponse>>
	{
		[Required]
		public int ID { get; set; }
		public GetOrderByIDModel(int iD)
		{
			ID = iD;
		}
	}
}
