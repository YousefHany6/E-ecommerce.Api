using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class UserPhoneNumber
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		public bool PhoneNumberIsActive { get; set; }

		public int UserID { get; set; }
		[ForeignKey(nameof(UserID))]
		public virtual User? User { get; set; }
	}
}
