using AutoMapper;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.DTO.UserModel;
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
		public void GetUsersRoleManagerMap()
		{
			CreateMap<User, GetManagersModel>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
				.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
				.ForMember(dest => dest.products, opt => opt.MapFrom(src => src.Products))
				.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.UserPhoneNumbers))
				.ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.UserAddresses.Select(s => s.Address)));



		}
		public void MapProductToProductModel()
		{
			CreateMap<Product, ProductModelResponse>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.DiscountName, opt => opt.MapFrom(src => src.Discount.Name))
				.ForMember(dest => dest.photos, opt => opt.MapFrom(src => src.ProductPhotos.Select(s => s.Url)))
				.ForMember(dest => dest.UserCreateProductName, opt => opt.MapFrom(src => src.User.FirstName+" "+src.User.LastName))
				.ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
				.ForMember(dest => dest.Discountpercentage, opt => opt.MapFrom(src => src.Discount.DiscountPercentage));
		}
		public void MapPhoneNumberforUser()
		{
			CreateMap<UserPhoneNumber, UserPhoneNumberModel>();
			CreateMap<UserPhoneNumber, string>();

		}
		public void MapUserAddresses()
		{
			CreateMap<UserAddress, string>();
		}
		public void MapEditManager()
		{
			CreateMap<User, ResponseEditManager>()
				.ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
				.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
				.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
				.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.ImageUrl))
				.ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.UserPhoneNumbers.Select(s=>s.PhoneNumber)))
				.ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.UserAddresses.Select(s => s.Address)));
		}
	}
}
