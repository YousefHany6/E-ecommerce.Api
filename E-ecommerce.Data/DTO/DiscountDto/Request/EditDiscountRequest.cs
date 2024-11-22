using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.DiscountDto.Request
{
	public class EditDiscountRequest
	{
		[Required]
		public int ID { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int DiscountValue { get; set; }

	}
}
