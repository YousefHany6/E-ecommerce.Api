using E_ecommerce.Core.Bases;
using E_ecommerce.Data.DTO.ProductModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Features.Product.Command.Model
{
	public class DeleteProductModel:IRequest<Response<ProductModelResponse>>
	{
		public int productid { get; set; }
		public DeleteProductModel(int productid)
		{
			this.productid = productid;
		}
	}
}
