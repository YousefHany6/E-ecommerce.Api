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
	public class ApplyDiscountOnOneProductModel:IRequest<Response<DiscountResponse>>
	{
		[Required]
		public int DiscountId { get; set; }
		[Required]
		public int ProductId { get; set; }
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DiscountExpire { get; set; }
	}
}
