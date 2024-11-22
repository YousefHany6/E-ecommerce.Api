
using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.ProductModel;
using E_ecommerce.Data.DTO.ProductModel.Request;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Command.Model
{
	public class AddProductModel:IRequest<Response<ProductModelResponse>>
	{
		public int userID { get; set; }
		public ProductModelRequest model { get; set; }

		public AddProductModel(int userID, ProductModelRequest model)
		{
			this.userID = userID;
			this.model = model;
		}
	}
}
