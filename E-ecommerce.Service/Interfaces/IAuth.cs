using E_ecommerce.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IAuth
	{
		Task<AuthModel> Register(Register register);
		Task<AuthModel> Login(Login login);
		Task<AuthModel> RefreshTokenAsync(string token);
	}
}
