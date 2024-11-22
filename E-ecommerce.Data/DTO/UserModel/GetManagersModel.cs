using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.ProductModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.UserModel
{
	public class GetManagersModel
	{
		public string? Id { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? ImageUrl { get; set; }
		public string? Gender { get; set; }
		public string? Email { get; set; }

		public ICollection<ProductModelResponse> products { get; set; } = new HashSet<ProductModelResponse>();
		public ICollection<UserPhoneNumberModel> PhoneNumbers { get; set; } = new HashSet<UserPhoneNumberModel>();
		public ICollection<string> Addresses { get; set; } = new HashSet<string>();
	}
}
