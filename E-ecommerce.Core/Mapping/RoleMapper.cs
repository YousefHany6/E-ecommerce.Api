using E_ecommerce.Data.DTO.AuthorizationRequest;
using E_ecommerce.Data.DTO.AuthorizationResponse;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Mapping
{
	public partial class Profiles
	{
		public void MapRoleTOUserRole()
		{
			CreateMap<Role, RoleResponse>()
				.ForMember(dest=>dest.RoleId,opt=>opt.MapFrom(s=>s.Id))
				.ForMember(dest=>dest.RoleName,opt=>opt.MapFrom(s=>s.Name));
		}

	}
}
