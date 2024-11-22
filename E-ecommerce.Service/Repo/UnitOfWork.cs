using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Infrastructure.Interfaces;
using E_ecommerce.Infrastructure.Repos;
using E_ecommerce.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationContext _context;
		public IGRepo<Cart> carts { get; private set; }
		public IGRepo<FavoriteCart> fcarts { get; private set; }
		public IGRepo<Category> Cat { get; private set; }
	public	IGRepo<UserClaim> UserClaim { get; private set; }


		public IGRepo<User> user {  get; private set; }

		public UnitOfWork(ApplicationContext context)
		{
			_context = context;
			carts = new GRepo<Cart>(_context);
			fcarts = new GRepo<FavoriteCart>(_context);
			Cat=new GRepo<Category>(_context);
			user=new GRepo<User>(_context);
			UserClaim= new GRepo<UserClaim>(_context);

		}
		public void Dispose()
		{
			_context.Dispose();
		}
		public async Task BeginTransactionAsync()
		{
			await _context.Database.BeginTransactionAsync();
		}
		public async Task RolleBack()
		{
			await _context.Database.RollbackTransactionAsync();
		}
		public async Task Commit()
		{
			await _context.Database.CommitTransactionAsync();
		}
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
