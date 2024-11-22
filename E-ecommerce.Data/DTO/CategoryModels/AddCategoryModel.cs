﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.CategoryModels
{
	public class AddCategoryModel
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		[Required]
		public IFormFile ImageFile { get; set; }
	}
}
