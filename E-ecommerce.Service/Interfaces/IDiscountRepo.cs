using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.DiscountDto.Request;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IDiscountRepo
	{
		Task<IQueryable<Discount>> GetAllDiscounts(string search);
		Task<DiscountError> AddDiscount(AddDiscoundRequest add);
		Task<DiscountError> EditDiscount(int discountid, EditDiscountRequest edit);
		Task<DiscountError> GetById(int discountid);
		Task<DiscountError> Delete(int discountid);
		Task<Error> ApplyDiscountOnAllProduct(int discountid, DateTime Expiretime);
		Task<Error> ApplyDiscountOnSpecificCategory(int discountid, int categoryid, DateTime Expiretime);
		Task<Error> ApplyDiscountOnOneProduct(int discountid, int productid, DateTime Expiretime);
		Task<Error> DeleteDiscountOnSpecificCategory(int categoryid);
		Task<Error> DeleteDiscountOnOneProduct(int productid);
		Task<Error> DeleteDiscountOnAllProduct();
	}
}
