using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.ProductModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Query.Model
{
	public class GetProductByIdModel:IRequest<Response<ProductModelResponse>>
	{
		public int ID { get; set; }
		public GetProductByIdModel(int iD)
		{
			ID = iD;
		}
	}
}
