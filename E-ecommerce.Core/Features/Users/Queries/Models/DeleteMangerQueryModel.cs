using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Users.Queries.Models
{
	public class DeleteMangerQueryModel : IRequest<Response<GetManagersModel>>
	{
		public int Id { get; set; }
		public DeleteMangerQueryModel(int id)
		{
			Id = id;
		}
	}
}
