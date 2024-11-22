using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Commands.Models
{
	public class AddUserInNewRoleModel:IRequest<Response<RoleResponse>>
	{
		public AddUserInNewRoleModel(int role_id, int user_id)
		{
			Role_Id= role_id;
			User_Id= user_id;
		}

		[Required]
		public int Role_Id { get; set; }
		[Required]
		public int User_Id { get; set; }

	}
}
