using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.Entites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface ICustomerRepo
	{
		Task<ErrorUser> AddCustomer(Register model);


	}
}
