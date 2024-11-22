using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.ProductModel
{
	public class ErrorProductResult
	{
		public bool Ok { get; set; }
		public string? Message_Error { get; set; }
		public Product? Product { get; set; }
	}
}
