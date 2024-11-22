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

namespace E_ecommerce.Core.Features.Authorization.Queries.Models
{
	public class ManageUserRolesDataModel: IRequest<Response<ManageUserRoles>>
	{
		[Required]
		public string User_Id { get; set; }
		public ManageUserRolesDataModel(string user_Id)
		{
			User_Id = user_Id;
		}
	}
}
