using AutoMapper;
using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace E_ecommerce.Service.Repo
{
	public class CustomerRepo : ICustomerRepo
	{
		private readonly UserManager<User> userManager;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork unitOfWork;
		private readonly IPhotoService photoRepo;

		public CustomerRepo(
			UserManager<User> _userManager,
			IWebHostEnvironment webHostEnvironment,
			IUnitOfWork unitOfWork,
			IPhotoService PhotoRepo
			)
		{
			userManager = _userManager;
			_webHostEnvironment = webHostEnvironment;
			this.unitOfWork = unitOfWork;
			photoRepo = PhotoRepo;
		}
		public async Task<ErrorUser> AddCustomer(Register model)
		{
			if (await userManager.FindByEmailAsync(model.Email) != null)
			{
				return new ErrorUser { Message = "Email Is Found" };
			}
			//-----------------------------
			var user = new User
			{
				FirstName = model.FName,
				LastName = model.LName,
				Email = model.Email,
				UserName = model.Email
			};
			//-----------------------------
			if (model.Photo == null)
			{
				if (model.Gender == Gender.Male)
				{
					user.ImageUrl = "DefaultMale.jpg";
				}
				else
				{
					user.ImageUrl = "DefaultFemale.jpg";

				}
			}
			else
			{
				//add photo in wwwroot

				user.ImageUrl = await photoRepo.AddPhoto(model.Photo, "UserImages");
			}
			//-----------------------------
			var R = await userManager.CreateAsync(user, model.Password);
			if (!R.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in R.Errors)
					errors += $"{error.Description},";
				return new ErrorUser { Message = errors, ok = false };
			}

			await userManager.AddToRoleAsync(user, Roles.Customer.ToString());

			//-----------------------------
			await unitOfWork.carts.Add(new Cart() { CustomerID = user.Id });
			await unitOfWork.fcarts.Add(new FavoriteCart() { CustomerID = user.Id });
			await unitOfWork.SaveChangesAsync();
			//-----------------------------
			return new ErrorUser
			{
				Message = "User Added",
				ok = true,
				User = user
			};
		}



	}
}

