using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.UserModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Users.Queries.Models
{
	public class GetUsersRoleManagerModel:IRequest<PaginatedResult<GetManagersModel>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public EnumOrderManager? order { get; set; }
	}
}
