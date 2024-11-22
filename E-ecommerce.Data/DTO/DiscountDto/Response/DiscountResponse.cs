using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.DiscountDto.Response
{
	public class DiscountResponse
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int DiscountValue { get; set; }
	}
}
