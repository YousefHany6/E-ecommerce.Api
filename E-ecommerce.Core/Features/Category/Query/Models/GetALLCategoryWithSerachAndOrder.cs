using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.CategoryModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Category.Query.Models
{
	public class GetALLCategoryWithSerachAndOrder:IRequest<PaginatedResult<ResponseCategoryModel>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? Search { get; set; }
		public EnumOrderCategory? order { get; set; }
	}
}
