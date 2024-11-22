using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Auth.Commands.Models
{
	public class RegisterUserCommand : IRequest<Response<AuthModel>>
	{
		public Register _register { get; set; }

		public RegisterUserCommand(Register register)
		{
			_register = register;
		}
	}
}
