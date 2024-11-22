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
	public class DeleteRoleModel : IRequest<Response<RoleResponse>>
	{
		[Required]
		public int Role_Id { get; set; }
		public DeleteRoleModel(int role_id)
		{
			Role_Id = role_id;
		}
	}
}
