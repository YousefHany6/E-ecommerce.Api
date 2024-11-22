using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.DiscountDto.Request;
using E_ecommerce.Data.DTO.DiscountDto.Response;
using E_ecommerce.Data.Entites;
using E_ecommerce.Data.ResourcesLocalization;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class DiscountRepo : IDiscountRepo
	{
		private readonly ApplicationContext context;
		private readonly IStringLocalizer<Resources> lo;

		public DiscountRepo(
			ApplicationContext context,
			IStringLocalizer<Resources> lo
	)
		{
			this.context = context;
			this.lo = lo;
		}
		public async Task<DiscountError> AddDiscount(AddDiscoundRequest add)
		{
			var dis = await context.Discounts.AsNoTracking().FirstOrDefaultAsync(s => s.Name == add.Name);
			if (dis != null)
			{
				return new DiscountError { Message = add.Name + " " + lo[ResourcesKeys.IsExisted] };
			}
			var newdis = new Discount()
			{
				Name = add.Name,
				DiscountPercentage = add.DiscountValue
			};
			try
			{
				await context.Discounts.AddAsync(newdis);
				await context.SaveChangesAsync();
			}
			catch (Exception)
			{

				return new DiscountError
				{ Message = lo[ResourcesKeys.BadRequest] };
			}

			return new DiscountError()
			{
				Discount = newdis,
				ok = true,
				Message = lo[ResourcesKeys.AddedSuccessfully]
			};
		}
		public async Task<DiscountError> Delete(int discountid)
		{
			var isexist = await GetById(discountid);
			if (isexist.ok == false)
				return isexist;
			context.Discounts.Remove(isexist.Discount);
			await context.SaveChangesAsync();
			isexist.Message = lo[ResourcesKeys.DeletedSuccessfully];
			return isexist;
		}
		public async Task<DiscountError> EditDiscount(int discountid, EditDiscountRequest edit)
		{
			var dis=await GetById(discountid);
			if (discountid != edit.ID|| dis.Discount is null)
			{
				return new DiscountError 
				{
					 Message="Discount"+" " + lo[ResourcesKeys.NotFound]
				};
			}
			dis.Discount.DiscountPercentage=edit.DiscountValue;
			dis.Discount.Name=edit.Name;
			context.Update(dis.Discount);
			await context.SaveChangesAsync();
			return new DiscountError { ok=true,Message=lo[ResourcesKeys.Successfully],Discount=dis.Discount };
		}
		public async Task<IQueryable<Discount>> GetAllDiscounts(string search)
		{
			var dis = context.Discounts.AsNoTracking().AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				dis = dis.Where(s => s.Name.Contains(search));
			}
			return dis;
		}
		public async Task<DiscountError> GetById(int discountid)
		{
			var dis = await context.Discounts.AsNoTracking().FirstOrDefaultAsync(s => s.ID == discountid);
			if (dis == null)
			{
				return new DiscountError { Message = discountid + " " + lo[ResourcesKeys.NotFound] };
			}
			return new DiscountError
			{
				Discount = dis,
				ok = true,
				Message = lo[ResourcesKeys.Successfully]
			};
		}
		public async Task<Error> ApplyDiscountOnAllProduct(int discountid, DateTime Expiretime)
		{
			var discount = await GetById(discountid);
			if (discount == null)
			{
				return new Error { Message = "Discount" + " " + lo[ResourcesKeys.NotFound] };
			}
			var pro = await context.Products.ToListAsync();
			if (pro == null)
			{
				return new Error { Message = "Products " + " " + lo[ResourcesKeys.NotFound] };
			}
			foreach (var Product in pro)
			{
				Product.DiscountID = discountid;
				Product.DiscountExpireDate = Expiretime;
				context.Products.Update(Product);
			}
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}
		public async Task<Error> ApplyDiscountOnOneProduct(int discountid, int productid, DateTime Expiretime)
		{
			var discount = await GetById(discountid);
			if (discount == null)
			{
				return new Error { Message = "Discount" + " " + lo[ResourcesKeys.NotFound] };
			}
			var pro = await context.Products.FirstOrDefaultAsync(s => s.Id == productid);
			if (pro == null)
			{
				return new Error { Message = "Product " + " " + lo[ResourcesKeys.NotFound] };
			}
			pro.DiscountID = discountid;
			pro.DiscountExpireDate = Expiretime;

			context.Products.Update(pro);
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}
		public async Task<Error> ApplyDiscountOnSpecificCategory(int discountid,int categoryid, DateTime Expiretime)
		{
			var discount = await GetById(discountid);
			if (discount == null)
			{
				return new Error { Message = "Discount" + " " + lo[ResourcesKeys.NotFound] };
			}
			var pro = await context.Products.Where(s=>s.CategoryID==categoryid).ToListAsync();
			if (pro == null)
			{
				return new Error { Message = "Products" + " " + lo[ResourcesKeys.NotFound] };
			}
			foreach (var Product in pro)
			{
				Product.DiscountID = discountid;
				Product.DiscountExpireDate = Expiretime;
				context.Products.Update(Product);
			}
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}
		public async Task<Error> DeleteDiscountOnAllProduct()
		{
			var pro = await context.Products.Where(s=>s.DiscountID.HasValue).ToListAsync();
			if (pro == null)
			{
				return new Error { Message = "Products " + " " + lo[ResourcesKeys.NotFound] };
			}
			foreach (var Product in pro)
			{
				Product.DiscountID = null;
				Product.DiscountExpireDate = null;
				context.Products.Update(Product);
			}
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}
		public async Task<Error> DeleteDiscountOnOneProduct(int productid)
		{
			var pro = await context.Products.FirstOrDefaultAsync(s => s.Id == productid);
			if (pro == null)
			{
				return new Error { Message = "Product " + " " + lo[ResourcesKeys.NotFound] };
			}
			pro.DiscountID = null;
			pro.DiscountExpireDate = null;

			context.Products.Update(pro);
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}
		public async Task<Error> DeleteDiscountOnSpecificCategory(int categoryid)
		{
			var pro = await context.Products.Where(s => s.CategoryID == categoryid&&s.DiscountID.HasValue).ToListAsync();
			if (pro == null)
			{
				return new Error { Message = "Products" + " " + lo[ResourcesKeys.NotFound] };
			}
			foreach (var Product in pro)
			{
				Product.DiscountID = null;
				Product.DiscountExpireDate = null;
				context.Products.Update(Product);
			}
			await context.SaveChangesAsync();
			return new Error
			{
				Message = lo[ResourcesKeys.Successfully]
							,
				ok = true
			};
		}


	}
}
