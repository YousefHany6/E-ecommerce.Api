using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationRequest;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Commands.Models
{
	public class EditRoleModel : IRequest<Response<RoleResponse>>
	{
		public EditRoleRequest model { get; set; }
		public EditRoleModel(EditRoleRequest model)
		{
			this.model = model;
		}
	}
}
