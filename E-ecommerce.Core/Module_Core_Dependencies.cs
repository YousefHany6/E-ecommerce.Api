using E_ecommerce.Service.Interfaces;
using E_ecommerce.Service.Repo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Core
{
	public static class Module_Core_Dependencies
	{
		public static IServiceCollection Add_Module_Core_Dependencies(this IServiceCollection services)
		{
			services.AddMediatR(a => a.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
	
			return services;
		}
	}
}
