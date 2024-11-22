﻿using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Discount.Command.Models
{
	public class ApplyDiscountOnAllProductModel : IRequest<Response<DiscountResponse>>
	{
		[Required]
		public int Discountid { get; set; }
		[Required]
		public DateTime DiscountExpire { get; set; }
	}
}