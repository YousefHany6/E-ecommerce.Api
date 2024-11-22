using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class UserClaim:IdentityUserClaim<int>
	{
	}
}
