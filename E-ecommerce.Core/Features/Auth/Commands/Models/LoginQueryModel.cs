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
    public class LoginQueryModel : IRequest<Response<AuthModel>>
    {
        public Login _login { get; set; }
        public LoginQueryModel(Login login)
        {
            _login = login;
        }

    }
}
