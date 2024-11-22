using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Entites
{
	public class Contact
	{
		[Key]
		public int ID { get; set; }
		[Required]
		public string FullName { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string Email { get; set; }
		[Phone ,Required]
		public string Phone { get; set; }

		public int UserID { get; set; }
		[ForeignKey(nameof(UserID))]
		public virtual User? User { get; set; }
	}
}
