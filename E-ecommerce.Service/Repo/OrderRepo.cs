using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.Order.Request;
using E_ecommerce.Data.DTO.Order.Response;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class OrderRepo : IOrderRepo
	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly ApplicationContext context;

		public OrderRepo(
			IStringLocalizer<Resources> lo,
			ApplicationContext context

			)
		{
			this.lo = lo;
			this.context = context;
		}
		public async Task<ErrorOrder> AddOrder(OrderRequest order, User customer)
		{
			using (var trans = await context.Database.BeginTransactionAsync())
			{
				try
				{
					if (order.Products.Count == 0)
					{
						await trans.RollbackAsync();
						return new ErrorOrder
						{
							ok = false,
							Message = $"Products not found."
						};
					}
					var newOrder = new Order
					{
						CustomerID = customer.Id,
						ProductsOrders = new List<ProductsOrder>()
					};

					foreach (var item in order.Products)
					{
						var product = await context.Products
																										.FirstOrDefaultAsync(s => s.Id == item.ProductID);

						if (product == null)
						{
							await trans.RollbackAsync();
							return new ErrorOrder
							{
								ok = false,
								Message = $"Product ID ({item.ProductID}) not found."
							};
						}

						if (product.Quantity < item.ProductQuantity || product.Quantity == 0 || item.ProductQuantity <= 0)
						{
							await trans.RollbackAsync();
							return new ErrorOrder
							{
								ok = false,
								Message = $"Product ID ({item.ProductID}) has insufficient stock."
							};
						}

						var orderProduct = new ProductsOrder
						{
							OrderID = newOrder.ID,
							ProductID = item.ProductID,
							ProductQuantity = item.ProductQuantity,
							Price = item.ProductQuantity * product.FinalPrice
						};

						product.Quantity -= item.ProductQuantity;
						newOrder.ProductsOrders.Add(orderProduct);
						newOrder.TotalPrice += orderProduct.Price;
					}

					await context.Orders.AddAsync(newOrder);
					context.Users.Update(customer);
					await context.SaveChangesAsync();

					await trans.CommitAsync();

					return new ErrorOrder
					{
						ok = true,
						Message = "Order added successfully.",
						OrderResponse = newOrder
					};
				}
				catch (Exception ex)
				{
					await trans.RollbackAsync();
					return new ErrorOrder
					{
						ok = false,
						Message = "An error occurred while adding the order. Please try again."
					};
				}
				finally
				{
					await trans.DisposeAsync();
				}
			}
		}
		public async Task<ErrorOrder> ChangeStatusOFOrder(List<int> ordersid, OrderStatus orderStatus)
		{
			if (ordersid.Count == 0 || string.IsNullOrEmpty(orderStatus.ToString()))
			{
				return new ErrorOrder
				{
					ok = false,
					Message = "Orders No Found . Please try again."
				};
			}
			foreach (var id in ordersid)
			{
				var order = await context.Orders.
					FirstOrDefaultAsync(s => s.ID == id);
				if (order == null)
				{
					return new ErrorOrder
					{
						ok = false,
						Message = $"Orders  Not Found .Please Try Again."
					};
				}
				order.OrderStatus = orderStatus;
			}
			await context.SaveChangesAsync();
			return new ErrorOrder
			{
				ok = true,
				Message = "Order Status Change successfully."
			};
		}
		public async Task<ErrorOrder> DeleteProductFromOrder(int orderid, List<int> ProductsId)
		{
			var order = await context.ProductsOrders.
				Where(s => ProductsId.Contains(s.ProductID) && s.OrderID.Equals(orderid))
				.ToListAsync();
			if (order == null)
			{
				return new ErrorOrder
				{
					Message = "Products Not Found In Order "
				};
			}
			var ord = await context.Orders.AsNoTracking().
				FirstOrDefaultAsync(s => s.ID == orderid);
			if (ord.OrderStatus == OrderStatus.charged)
			{
				return new ErrorOrder
				{
					Message = "It is not possible to delete Product  from the order because it has been shipped"
				};

			}
			context.RemoveRange(order);
			await context.SaveChangesAsync();
			return new ErrorOrder
			{
				ok = true,
				Message = lo[ResourcesKeys.DeletedSuccessfully]
			};
		}
		public async Task<ErrorOrder> AddProductInOrder(int orderId, OrderProduct productOrder)
		{
			var order = await context.Orders
							.Include(o => o.ProductsOrders)
							.FirstOrDefaultAsync(o => o.ID == orderId);

			if (order == null)
				return new ErrorOrder { Message = "Order Not Found" };
			if (order.OrderStatus == OrderStatus.charged
				|| order.OrderStatus == OrderStatus.Order_Canceled
				|| order.OrderStatus == OrderStatus.decline)
			{
				return new ErrorOrder
				{
					Message = "Order"
																											+ " " + order.OrderStatus.ToString()
																											+ " .Please Create New Order"
				};

			}
			var product = await context.Products
							.FirstOrDefaultAsync(p => p.Id == productOrder.ProductID);

			if (product == null)
				return new ErrorOrder { Message = "Product Not Found" };

			if (productOrder.ProductQuantity <= 0 || productOrder.ProductQuantity > product.Quantity || product.Quantity == 0)
				return new ErrorOrder { Message = "Invalid or insufficient product quantity" };

			var newProductOrder = new ProductsOrder
			{
				OrderID = orderId,
				ProductID = product.Id,
				ProductQuantity = productOrder.ProductQuantity,
			};

			order.ProductsOrders.Add(newProductOrder);
			product.Quantity -= productOrder.ProductQuantity;
			order.TotalPrice += product.FinalPrice * productOrder.ProductQuantity;

			using var transaction = await context.Database.BeginTransactionAsync();

			try
			{
				context.Orders.Update(order);
				context.Products.Update(product);
				await context.SaveChangesAsync();
				await transaction.CommitAsync();
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}

			return new ErrorOrder
			{
				ok = true,
				Message = lo[ResourcesKeys.AddedSuccessfully],
				OrderResponse = order
			};
		}
		public async Task<IQueryable<Order>> GetAllOrders(OrderStatus? orderStatus = null)
		{
			var orders = context.Orders
							.AsNoTracking()
							.Include(s => s.ProductsOrders)
							.AsQueryable();

			if (orderStatus.HasValue && orders.Any())
			{
				orders = orders.Where(g => g.OrderStatus == orderStatus.Value);
			}

			return orders;
		}
		public async Task<ErrorOrder> CancelOrder(int orderid)
		{
			var ord = await context.Orders.
					FirstOrDefaultAsync(s => s.ID == orderid);
			if (ord == null)
			{
				return new ErrorOrder
				{
					ok = false,
					Message = $"Order  Not Found .Please Try Again."
				};
			}
			var order = await context.Orders.AsNoTracking().
					FirstOrDefaultAsync(s => s.ID == orderid);
			if (order.OrderStatus == OrderStatus.charged)
			{
				return new ErrorOrder
				{
					Message = "It is not possible to Cancel Order  because it has been shipped"
				};
			}
			var newlist = new List<int>();
			newlist.Add(orderid);
			var cancelorder = await ChangeStatusOFOrder(newlist, OrderStatus.Order_Canceled);
			return cancelorder;
		}
		public async Task<ErrorOrder> DeleteOrders(List<int> orderids)
		{
			var ords = await context.Orders.
							Where(s => orderids.Contains(s.ID)).ToListAsync();
			if (!ords.Any())
			{
				return new ErrorOrder
				{
					ok = false,
					Message = $"Orders Not Found .Please Try Again."
				};
			}
			context.Orders.RemoveRange(ords);
			await context.SaveChangesAsync();
			return new ErrorOrder { ok = true, Message = lo[ResourcesKeys.DeletedSuccessfully] };
		}
		public async Task<ErrorOrder> GetOrderByID(int orderid)
		{
			var ord = await context.Orders.
				Include(s => s.ProductsOrders).
				ThenInclude(s => s.Product).
					FirstOrDefaultAsync(s => s.ID == orderid);
			if (ord == null)
			{
				return new ErrorOrder
				{
					ok = false,
					Message = $"Order  Not Found .Please Try Again."
				};
			}
			return new ErrorOrder
			{
				ok = true,
				Message = lo[ResourcesKeys.Successfully]
				,
				OrderResponse = ord
			}; ;
		}
	}
}
