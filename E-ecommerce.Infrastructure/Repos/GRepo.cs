using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Infrastructure.Repos
{
	public class GRepo<T> : IGRepo<T> where T : class
	{
		private readonly ApplicationContext _context;

		public GRepo(ApplicationContext context)
		{
			_context = context;

		}
		public async Task<T> Add(T item)
		{
			await _context.Set<T>().AddAsync(item);
			return item;
		}

		public async Task Delete(int id)
		{
			var entity = await _context.Set<T>().FindAsync(id);
			 _context.Set<T>().Remove(entity);
		}

		public async Task<T> Edit(T edititem)
		{
			_context.Set<T>().Update(edititem);
			return edititem;
		}

		public async Task<IEnumerable<T>> Get()
		{
			return await _context.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T> GetBYId(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

	}
}
