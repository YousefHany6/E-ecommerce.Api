using AutoMapper;
using E_ecommerce.Data.DTO.CategoryModels;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Mapping
{
	public partial class Profiles
	{
		public void categorymap()
		{
			CreateMap<Category, ResponseCategoryModel>()
				.ForMember(s => s.Id, opt => opt.MapFrom(s => s.Id))
				.ForMember(s => s.Name, opt => opt.MapFrom(s => s.Name))
				.ForMember(s => s.Description, opt => opt.MapFrom(s => s.Description))
				.ForMember(s => s.Products, opt => opt.MapFrom(s => s.Products));
		}
	}
}
