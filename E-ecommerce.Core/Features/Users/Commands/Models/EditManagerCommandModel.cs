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
	public class EditManagerCommandModel : IRequest<Response<ResponseEditManager>>
	{
		public int managerid { get; set; }

		public EditManagerModel model { get; set; }
		public EditManagerCommandModel(int id,EditManagerModel managerModel)
		{
			managerid = id;
			model = managerModel;
		}
	}
}
