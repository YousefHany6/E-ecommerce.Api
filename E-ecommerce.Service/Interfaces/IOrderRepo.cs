using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.Order.Request;
using E_ecommerce.Data.DTO.Order.Response;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IOrderRepo
	{
		Task<IQueryable<Order>> GetAllOrders(OrderStatus? orderStatus = null);
		Task<ErrorOrder>AddOrder(OrderRequest order, User Customer);
		Task<ErrorOrder> AddProductInOrder(int orderId, OrderProduct productOrder);
		Task<ErrorOrder> DeleteProductFromOrder(int orderid, List<int> ProductsId);
		Task<ErrorOrder> CancelOrder(int orderid);
		Task<ErrorOrder>ChangeStatusOFOrder(List<int> ordersid, OrderStatus orderStatus);
		Task<ErrorOrder> GetOrderByID(int orderid);
		Task<ErrorOrder> DeleteOrders(List<int> orderids);
	}
}
