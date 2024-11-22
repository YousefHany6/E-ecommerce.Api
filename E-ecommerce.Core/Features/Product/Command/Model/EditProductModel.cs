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
	public class EditProductModel:IRequest<Response<ProductModelResponse>>
	{
		public EditProductmodelRequest	 model { get; set; }
		public int productid { get; set; }

		public EditProductModel(EditProductmodelRequest model, int productid)
		{
			this.model = model;
			this.productid = productid;
		}
	}
}
