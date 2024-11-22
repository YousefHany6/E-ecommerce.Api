using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.Constant
{
	public enum OrderStatus
	{
		decline,
		Accept,
		charged,
		pending,
		Delivered_Done,
		Order_Canceled
	}
}
