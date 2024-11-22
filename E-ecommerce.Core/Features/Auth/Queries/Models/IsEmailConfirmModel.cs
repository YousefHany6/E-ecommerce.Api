using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Auth.Queries.Models
{
	public class IsEmailConfirmModel:IRequest<Response<IsEmailConfirmResponse>>
	{
		public int UserId { get; set; }
		public IsEmailConfirmModel(int userId)
		{
			UserId = userId;
		}
	}
}
