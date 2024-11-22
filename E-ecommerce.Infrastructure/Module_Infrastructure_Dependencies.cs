using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Interfaces;
using E_ecommerce.Infrastructure.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace E_ecommerce.Infrastructure
{
	public static class Module_Infrastructure_Dependencies
	{	
		public static IServiceCollection Add_Infrastructure_Dependencies(this IServiceCollection services)
		{
			services.AddTransient(typeof(IGRepo<>), typeof(GRepo<>));
			return services;
		}
	}
}
