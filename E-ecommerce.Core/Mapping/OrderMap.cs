using E_ecommerce.Data.DTO.Order.Response;
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
		public void ordemap()
		{
			CreateMap<Order, OrderResponse>()
				.ForMember(s => s.CustomerName, s => s.MapFrom(s => s.Customer.FirstName+" "+s.Customer.LastName))
				.ForMember(s => s.OrderStatus, s => s.MapFrom(s => s.OrderStatus))
				.ForMember(s => s.OrderDate, s => s.MapFrom(s => s.OrderDate))
				.ForMember(s => s.Products_IN_Order, s => s.MapFrom(s => s.ProductsOrders))
				.ForMember(s => s.TotalPrice, s => s.MapFrom(s => s.TotalPrice))
				.ForMember(s => s.ID, s => s.MapFrom(s => s.ID));
		}
		public void Allordersmap()
		{
			CreateMap<Order, AllOrdersResponse>()
				.ForMember(s => s.CustomerName, s => s.MapFrom(s => s.Customer.FirstName + " " + s.Customer.LastName))
				.ForMember(s => s.CustomerId, s => s.MapFrom(s => s.Customer.Id))
				.ForMember(s => s.OrderStatus, s => s.MapFrom(s => s.OrderStatus))
				.ForMember(s => s.OrderDate, s => s.MapFrom(s => s.OrderDate))
				.ForMember(s => s.TotalPrice, s => s.MapFrom(s => s.TotalPrice))
				.ForMember(s => s.ID, s => s.MapFrom(s => s.ID));
		}
		public void productorders()
		{
			CreateMap<ProductsOrder, ProductOrderResponse>()
				.ForMember(s => s.ProductID, s => s.MapFrom(s => s.ProductID))
				.ForMember(s => s.Price, s => s.MapFrom(s => s.Price))
				.ForMember(s => s.ProductName, s => s.MapFrom(s => s.Product.Name))
				.ForMember(s => s.ProductQuantity, s => s.MapFrom(s => s.ProductQuantity));
		}
	}
}
