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
	public class DeleteProductFromOrderModel : IRequest<Response<OrderResponse>>
	{
		[Required]
		public List<int> Products_Id { get; set; }
		[Required]
		public int Order_Id { get; set; }
		public DeleteProductFromOrderModel(List<int> product_id, int orderid)
		{
			Products_Id = product_id;
			Order_Id = orderid;
		}
	}
}
