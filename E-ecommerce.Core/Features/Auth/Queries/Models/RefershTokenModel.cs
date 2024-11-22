using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Auth.Queries.Models
{
	public class RefershTokenModel:IRequest<Response<AuthModel>>
	{
		public string _refreshtoken { get; set; }
		public RefershTokenModel(string refreshtoken)
		{
			_refreshtoken = refreshtoken;
		}
	}
}
