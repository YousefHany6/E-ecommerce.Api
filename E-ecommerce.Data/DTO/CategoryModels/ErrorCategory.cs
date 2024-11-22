using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.CategoryModels
{
	public class ErrorCategory
	{
		public string? Message { get; set; }
		public bool ok { get; set; }

		public Category? cat { get; set; }
	}
}
