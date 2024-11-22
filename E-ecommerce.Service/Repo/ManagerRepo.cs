using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO.UserModel;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.Entites;
using E_ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_ecommerce.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Microsoft.Extensions.Localization;
using E_ecommerce.Data.ResourcesLocalization;

namespace E_ecommerce.Service.Repo
{
	public class ManagerRepo : IManagerRepo
	{


		private readonly UserManager<User> userManager;
		private readonly ApplicationContext context;
		private readonly IPhotoService PhotoRepo;
		private readonly IUnitOfWork unitOfWork;
		private readonly IStringLocalizer<Resources> lo;

		public ManagerRepo(
			UserManager<User> _userManager,
			ApplicationContext context,
			IPhotoService PhotoRepo,
			IUnitOfWork unitOfWork,
			IStringLocalizer<Resources> lo

			)
		{
			userManager = _userManager;
			this.context = context;
			this.PhotoRepo = PhotoRepo;
			this.unitOfWork = unitOfWork;
			this.lo = lo;
		}
		public async Task<ErrorUser> AddManager(AddManagerModel model)
		{
			if (await userManager.FindByEmailAsync(model.Email) != null)
			{
				return new ErrorUser { Message = lo[ResourcesKeys.EmailIsFound] };
			}
			//-----------------------------
			var user = new User
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
				UserName = model.Email,
				EmailConfirmed=true
			};
			//-----------------------------
			if (model.Image == null)
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

				user.ImageUrl = await PhotoRepo.AddPhoto(model.Image, "UserImages");
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
			//-----------------------------
			await userManager.AddToRoleAsync(user, Roles.Manager.ToString());
			if (model.PhoneNumbers != null)
			{
				var phones = new List<UserPhoneNumber>();
				foreach (var item in model.PhoneNumbers)
				{
					phones.Add(new UserPhoneNumber() { PhoneNumber = item, PhoneNumberIsActive = true, UserID = user.Id });
				}
				user.UserPhoneNumbers.AddRange(phones);
			}
			if (model.Addresses != null)
			{
				var Addresses = new List<UserAddress>();
				foreach (var item in model.Addresses)
				{
					Addresses.Add(new UserAddress() { Address = item, UserID = user.Id });
				}
				user.UserAddresses.AddRange(Addresses);
			}
			//-----------------------------
			await userManager.UpdateAsync(user);
			//-----------------------------
			return new ErrorUser
			{
				Message = lo[ResourcesKeys.AddedSuccessfully],
				ok = true,
				User = user
			};
		}
		public async Task<IQueryable<User>> GetUsersWithFilterSearchAndOrderAsync(EnumOrderManager? order, string? search)
		{
			var user_Role_Manager = await userManager.GetUsersInRoleAsync(Roles.Manager.ToString());

			var managers = context.Users
				.Where(x => user_Role_Manager.Select(s => s.Id).ToList().Contains(x.Id))
				.Include(s => s.UserAddresses)
				.Include(s => s.UserPhoneNumbers)
				.Include(s => s.Products)
				.ThenInclude(s => s.Category)
					.Include(s => s.Products)
				.ThenInclude(s => s.Discount)
			.AsQueryable()
			.AsNoTracking();

			if (!string.IsNullOrEmpty(search))
			{
				managers = managers.Where(
					x => x.FirstName.ToLower().Contains(search.ToLower())
				|| x.LastName.ToLower().Contains(search.ToLower())
				|| x.Email.ToLower().Contains(search.ToLower()));
			}
			if (!string.IsNullOrEmpty(order.ToString()))
			{
				switch (order)
				{
					case EnumOrderManager.FName:
						managers = managers.OrderBy(x => x.FirstName);
						break;
					case EnumOrderManager.LName:
						managers = managers.OrderBy(x => x.LastName);
						break;
					case EnumOrderManager.Email:
						managers = managers.OrderBy(x => x.Email);
						break;
					case EnumOrderManager.UserId:
						managers = managers.OrderBy(x => x.Id);
						break;

				}
			}

			return managers;
		}
		public async Task<ErrorUser> GetManagerById(int id)
		{
			var manager = await context.Users.
				Include(s => s.UserAddresses)
				.Include(s => s.UserPhoneNumbers)
				.Include(s => s.Products)
			.ThenInclude(s => s.Category)
			.Include(s => s.Products)
				.ThenInclude(s => s.Discount)
				.FirstOrDefaultAsync(x => x.Id.Equals(id));
			if (manager is null || await userManager.IsInRoleAsync(manager, Roles.Manager.ToString()) == false)
			{
				return new ErrorUser { Message = lo[ResourcesKeys.NotFound] };
			}
			return new ErrorUser { User = manager, ok = true };
		}

		public async Task<ErrorUser> DeleteManger(int id)
		{
			var manager = await context.Users.
					Include(s => s.UserAddresses)
					.Include(s => s.UserPhoneNumbers)
					.Include(s => s.Products)
				.ThenInclude(s => s.Category)
				.Include(s => s.Products)
					.ThenInclude(s => s.Discount)
					.FirstOrDefaultAsync(x => x.Id.Equals(id));
			if (manager is null || await userManager.IsInRoleAsync(manager, Roles.Manager.ToString()) == false)
			{
				return new ErrorUser { Message = lo[ResourcesKeys.NotFound] };
			}
			await userManager.DeleteAsync(manager);
			return new ErrorUser { User = manager, ok = true };
		}

		public async Task<ErrorUser> EditManager(int id, EditManagerModel managerEdit)
		{
			var oldManager = await GetManagerById(id);
			if (oldManager.User == null || id != managerEdit.ManagerId)
			{
				return new ErrorUser { Message = oldManager.Message, ok = false };
			}

			using (var transaction = await context.Database.BeginTransactionAsync())
			{
				try
				{
					oldManager.User.FirstName = managerEdit.FirstName;
					oldManager.User.LastName = managerEdit.LastName;
					oldManager.User.Email = managerEdit.Email;
					oldManager.User.UserName = managerEdit.Email;
					oldManager.User.Gender = managerEdit.Gender;
					oldManager.User.EmailConfirmed = true;

					// Update addresses
					if (managerEdit.Addresses != null)
					{
						var existingAddresses = new HashSet<string>(oldManager.User.UserAddresses.Select(ua => ua.Address));
						var newAddresses = new HashSet<string>(managerEdit.Addresses);

						if (!existingAddresses.SetEquals(newAddresses))
						{
							context.UserAddresses.RemoveRange(oldManager.User.UserAddresses);
							oldManager.User.UserAddresses = newAddresses.Select(address => new UserAddress { Address = address, UserID = id }).ToList();
						}
					}

					// Update phone numbers
					if (managerEdit.PhoneNumbers != null)
					{
						var existingPhoneNumbers = new HashSet<string>(oldManager.User.UserPhoneNumbers.Select(up => up.PhoneNumber));
						var newPhoneNumbers = new HashSet<string>(managerEdit.PhoneNumbers);

						if (!existingPhoneNumbers.SetEquals(newPhoneNumbers))
						{
							context.Set<UserPhoneNumber>().RemoveRange(oldManager.User.UserPhoneNumbers);
							oldManager.User.UserPhoneNumbers = newPhoneNumbers.Select(phone => new UserPhoneNumber { PhoneNumber = phone, PhoneNumberIsActive = true, UserID = id }).ToList();
						}
					}

					// Handle image update
					if (managerEdit.Image != null
									&& managerEdit.Image.FileName != DefaultPhoto.PhotoMale
									&& managerEdit.Image.FileName != DefaultPhoto.PhotoFemale
									&& managerEdit.Image.FileName != oldManager.User.ImageUrl)
					{
						await PhotoRepo.DeletePhoto(oldManager.User.ImageUrl, DefaultPhoto.UserFolder);
						var imageUrl = await PhotoRepo.AddPhoto(managerEdit.Image, DefaultPhoto.UserFolder);
						oldManager.User.ImageUrl = imageUrl;
					}

					var result = await userManager.UpdateAsync(oldManager.User);
					if (!result.Succeeded)
					{
						await context.Database.RollbackTransactionAsync();
						return new ErrorUser { Message = string.Join(", ", result.Errors.Select(e => e.Description)), ok = false };
					}

					await context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
				catch (Exception ex)
				{
					await context.Database.RollbackTransactionAsync();
					return new ErrorUser { Message = ex.Message, ok = false };
				}
			}

			return new ErrorUser { User = oldManager.User, ok = true };
		}
	}
}
