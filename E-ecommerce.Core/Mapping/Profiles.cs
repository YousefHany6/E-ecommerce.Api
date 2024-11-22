using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Mapping
{
	public partial class Profiles : Profile
	{
		public Profiles()
		{
			//Manager
			GetUsersRoleManagerMap();
			MapProductToProductModel();
			MapPhoneNumberforUser();
			MapUserAddresses();
			MapEditManager();
			//Role
			MapRoleTOUserRole();
			//Category
			categorymap();
			//Discount
			MapDiscount();
			//order
			ordemap();
			productorders();
			Allordersmap();
		}
	}
}
