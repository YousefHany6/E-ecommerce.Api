using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationRequest;
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
	public class AddRoleModel:IRequest<Response<RoleResponse>>
	{
		[Required]
		public string Role_Name { get; set;}
		public AddRoleModel(string role_Name)
		{
			Role_Name = role_Name;
		}
	}
}
