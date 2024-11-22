using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Users.Commands.Models
{
	public class AddManagerCommandModel:IRequest<Response<AddManagerModel>>
	{
		public AddManagerModel Model { get; set; }

		public AddManagerCommandModel(AddManagerModel model)
		{
			Model = model;
		}
	}
}
