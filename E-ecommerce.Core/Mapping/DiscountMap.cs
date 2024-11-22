using AutoMapper;
using E_ecommerce.Data.DTO.DiscountDto.Response;
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
		public void MapDiscount()
		{
			CreateMap<Discount, DiscountResponse>()
			.ForMember(s => s.ID, opt => opt.MapFrom(src => src.ID))
			.ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(s => s.DiscountValue, opt => opt.MapFrom(src => src.DiscountPercentage));
		}
	}
}
