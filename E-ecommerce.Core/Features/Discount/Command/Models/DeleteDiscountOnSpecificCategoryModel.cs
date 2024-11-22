﻿using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Command.Models
{
	public class DeleteDiscountOnSpecificCategoryModel:IRequest<Response<DiscountResponse>>
	{
		public int CategoryID { get; set; }

		public DeleteDiscountOnSpecificCategoryModel(int categoryID)
		{
			CategoryID = categoryID;
		}
	}
}
