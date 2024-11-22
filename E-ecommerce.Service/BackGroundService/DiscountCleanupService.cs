using E_ecommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.BackGroundService
{
	public class DiscountCleanupService : BackgroundService
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly ILogger<DiscountCleanupService> _logger;

		public DiscountCleanupService(IServiceProvider serviceProvider, ILogger<DiscountCleanupService> logger)
		{
			_serviceProvider = serviceProvider;
			_logger = logger;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = _serviceProvider.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
					var now = DateTime.UtcNow;
					var expiredDiscounts = await dbContext.Products
									.Where(p => p.DiscountExpireDate <= now)
									.ToListAsync(stoppingToken);

					foreach (var product in expiredDiscounts)
					{
						product.DiscountID = null;
						product.Discount = null;
						product.DiscountExpireDate = null;
					}

					await dbContext.SaveChangesAsync(stoppingToken);
				}

				_logger.LogInformation("Discount cleanup task completed.");

				await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken); // Adjust interval as needed
			}
		}
	}

}
