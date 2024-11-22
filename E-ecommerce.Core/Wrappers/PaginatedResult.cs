using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Wrappers
{
	public class PaginatedResult<T>
	{
		public PaginatedResult(List<T> data)
		{
			BaseData = data;
		}
		public List<T> BaseData { get; set; }

		internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
		{
			BaseData = data;
			CurrentPage = page;
			Succeeded = succeeded;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			TotalCount = count;
		}

		public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
		{
			return new(true, data, null, count, page, pageSize);
		}

		public int CurrentPage { get; set; }

		public int TotalPages { get; set; }

		public int TotalCount { get; set; }

		public object Meta { get; set; }

		public int PageSize { get; set; }

		public bool HasPreviousPage => CurrentPage > 1;

		public bool HasNextPage => CurrentPage < TotalPages;

		public string Message { get; set; } 

		public bool Succeeded { get; set; }
	}
}
