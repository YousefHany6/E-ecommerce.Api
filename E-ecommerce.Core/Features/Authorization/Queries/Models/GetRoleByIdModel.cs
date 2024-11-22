using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Queries.Models
{
	public class GetRoleByIdModel : IRequest<Response<RoleResponse>>
	{
		[Required]
		public int Id { get; set; }
		public GetRoleByIdModel(int id)
		{
			Id = id;
		}
	}
}
