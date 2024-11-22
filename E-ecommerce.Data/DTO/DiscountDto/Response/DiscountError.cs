using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.DiscountDto.Response
{
	public class DiscountError
	{
		public string? Message { get; set; }
		public bool ok { get; set; }
		public Discount? Discount { get; set; }
	}
}
