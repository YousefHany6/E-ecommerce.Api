using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.ProductModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Query.Model
{
	public class GetAllProductsmodel : IRequest<Response<PaginatedResult<ProductModelResponse>>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public ProductEnumOrder? order { get; set; }
	}
}
