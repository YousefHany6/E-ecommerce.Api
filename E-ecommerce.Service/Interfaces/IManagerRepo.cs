using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IManagerRepo
	{
		Task<IQueryable<User>> GetUsersWithFilterSearchAndOrderAsync(EnumOrderManager? order, string? search);
		Task<ErrorUser> AddManager(AddManagerModel model);
		Task<ErrorUser> GetManagerById(int id);
		Task<ErrorUser> DeleteManger(int id);
		Task<ErrorUser> EditManager(int id, EditManagerModel managerEdit);
	}
}
