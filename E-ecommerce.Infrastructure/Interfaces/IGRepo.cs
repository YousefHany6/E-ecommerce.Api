using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Infrastructure.Interfaces
{
	public interface IGRepo<T> where T : class
	{
		Task<IEnumerable<T>> Get();
		Task<T> GetBYId(int id);
		Task<T> Add(T item);
		Task<T> Edit(T edititem);
		Task Delete(int id);
		//Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
		//IQueryable<T> FindAllAsync(string[] includes = null);
		//IQueryable<T> FindAllAsyncWithFilter(Expression<Func<T, bool>> criteria, string[] includes = null);
			}
}
