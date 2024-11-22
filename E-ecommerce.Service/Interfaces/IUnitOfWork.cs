using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IGRepo<Cart> carts { get; }
		IGRepo<FavoriteCart> fcarts { get; }
		IGRepo<Category> Cat { get; }
		IGRepo<User> user { get; }
		IGRepo<UserClaim> UserClaim { get; }
		Task SaveChangesAsync();
		Task Commit();
		Task RolleBack();
		Task BeginTransactionAsync();
	}
}
