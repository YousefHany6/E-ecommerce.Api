using E_ecommerce.Infrastructure.Interfaces;
using E_ecommerce.Infrastructure.Repos;
using E_ecommerce.Service.BackGroundService;
using E_ecommerce.Service.Interfaces;
using E_ecommerce.Service.Repo;
using Microsoft.Extensions.DependencyInjection;

namespace E_ecommerce.Service
{
	public static class Module_Service_Dependencies
	{
		public static IServiceCollection Add_Module_Service_Dependencies(this IServiceCollection services)
		{
			services.AddTransient<IAuth, Auth >();
			services.AddTransient<ICustomerRepo, CustomerRepo >();
			services.AddTransient<IManagerRepo, ManagerRepo >();
			services.AddTransient<IUnitOfWork, UnitOfWork >();
			services.AddTransient<ICategoryRepo, CategoryRepo >();
			services.AddTransient<IAuthorizationService, AuthorizationService>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<IProductService, ProductService>();
			services.AddTransient<IPhotoService, PhotoService>();
			services.AddTransient<IDiscountRepo, DiscountRepo>();
			services.AddTransient<IOrderRepo, OrderRepo>();
			services.AddHostedService<DiscountCleanupService>();
			return services;
		}
	}
}
