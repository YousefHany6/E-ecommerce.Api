using Azure.Core;
using E_ecommerce.Data.ResourcesLocalization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core.Bases
{
	public class ResponseHandler
	{
		private readonly IStringLocalizer<Resources> lo;

		public ResponseHandler(IStringLocalizer<Resources> lo)
		{
			this.lo = lo;
		}
		public Response<T> Deleted<T>(T Entity)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = lo[ResourcesKeys.DeletedSuccessfully],
				Data=Entity
			};
		}
		public Response<T> Success<T>(T entity, object Meta = null, string Message = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.OK,
				Succeeded = true,
				Message = Message == null ? lo[ResourcesKeys.AddedSuccessfully] : Message,
				Meta = Meta
			};
		}

		public Response<T> Unauthorized<T>()
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.Unauthorized,
				Succeeded = true,
				Message = lo[ResourcesKeys.UnAuthorized]
			};
		}
		public Response<T> BadRequest<T>(string Message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.BadRequest,
				Succeeded = false,
				Message = Message == null ? lo[ResourcesKeys.BadRequest] : Message
			};
		}

		public Response<T> NotFound<T>(string message = null)
		{
			return new Response<T>()
			{
				StatusCode = System.Net.HttpStatusCode.NotFound,
				Succeeded = false,
				Message = message == null ? lo[ResourcesKeys.NotFound] : message
			};
		}

		public Response<T> Created<T>(T entity, object Meta = null)
		{
			return new Response<T>()
			{
				Data = entity,
				StatusCode = System.Net.HttpStatusCode.Created,
				Succeeded = true,
				Message = lo[ResourcesKeys.AddedSuccessfully],
				Meta = Meta
			};
		}
	}

}
