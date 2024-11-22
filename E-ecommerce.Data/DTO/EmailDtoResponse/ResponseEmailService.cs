using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Data.DTO.EmailDtoResponse
{
	public class ResponseEmailService
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public SendData? data { get; set; }


	}
	public class SendData
	{
		public string? Email { get; set; }
		public string? EmailMessage { get; set; }

	}
}