using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Authorization.Queries.Models
{
	public class GetRolesListModel : IRequest<Response<IEnumerable<RoleResponse>>>
	{
	}
}
