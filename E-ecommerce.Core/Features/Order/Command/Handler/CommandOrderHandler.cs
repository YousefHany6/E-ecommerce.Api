using AutoMapper;
using E_ecommerce.Core.Bases;
using E_ecommerce.Core.Features.Order.Command.Models;
using E_ecommerce.Core.Features.Order.Query.Models;
using E_ecommerce.Core.Wrappers;
using E_ecommerce.Data.DTO.Order.Response;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Service.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Order.Command.Handler
{
	public class CommandOrderHandler : ResponseHandler,
		                                  IRequestHandler<AddOrderModel, Response<OrderResponse>>,
		                                  IRequestHandler<AddProductInOrderModel, Response<OrderResponse>>,
		                                  IRequestHandler<CancelOrderModel, Response<OrderResponse>>,
		                                  IRequestHandler<ChangeStatusOFOrderModel, Response<OrderResponse>>,
		                                  IRequestHandler<DeleteProductFromOrderModel, Response<OrderResponse>>,
		                                  IRequestHandler<DeleteOrdersModel, Response<OrderResponse>>,
																																				IRequestHandler<GetOrderByIDModel, Response<OrderResponse>>

	{
		private readonly IStringLocalizer<Resources> lo;
		private readonly IOrderRepo orderRepo;
		private readonly IMapper mapper;
		private readonly UserManager<User> userManager;

		public CommandOrderHandler(
			IStringLocalizer<Resources> lo,
			IOrderRepo orderRepo,
			IMapper mapper,
			UserManager<User> userManager) : base(lo)
		{
			this.lo = lo;
			this.orderRepo = orderRepo;
			this.mapper = mapper;
			this.userManager = userManager;
		}

		public async Task<Response<OrderResponse>> Handle(AddOrderModel request, CancellationToken cancellationToken)
		{
			var cust = await userManager.FindByIdAsync(request.userid.ToString());
			if (cust is null)
			{
				return NotFound<OrderResponse>("Customer Not Found");
			}
			var req = await orderRepo.AddOrder(request.order,cust);
			if (req.ok==false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map= mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map,Message:req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(AddProductInOrderModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.AddProductInOrder(request.Order_Id,request.Products);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(CancelOrderModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.CancelOrder(request.Order_Id);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(ChangeStatusOFOrderModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.ChangeStatusOFOrder(request.Orders_Id,request.OrderStatus);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(DeleteProductFromOrderModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.DeleteProductFromOrder(request.Order_Id, request.Products_Id);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(DeleteOrdersModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.DeleteOrders(request.Orders_ID);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}

		public async Task<Response<OrderResponse>> Handle(GetOrderByIDModel request, CancellationToken cancellationToken)
		{
			var req = await orderRepo.GetOrderByID(request.ID);
			if (req.ok == false)
			{
				return BadRequest<OrderResponse>(req.Message);
			}
			var map = mapper.Map<OrderResponse>(req.OrderResponse);

			return Success(map, Message: req.Message);
		}
	}
}
