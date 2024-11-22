using E_ecommerce.Data.Constant;
using E_ecommerce.Data.DTO;
using E_ecommerce.Data.Entites;
using E_ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class Auth : IAuth
	{
		private readonly UserManager<User> userManager;
		private readonly RoleManager<Role> roleManager;
		private readonly ICustomerRepo userRepo;
		private readonly JWTModel jwt;

		public Auth(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IOptions<JWTModel> jwt,
			ICustomerRepo userRepo)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
			this.userRepo = userRepo;
			this.jwt = jwt.Value;
		}

		public async Task<AuthModel> Login(Login login)
		{
			var auth = new AuthModel();
			var user = await userManager.FindByEmailAsync(login.Email);
			if (user == null || !await userManager.CheckPasswordAsync(user, login.Password))
			{
				auth.Message = "Email or Password is incorrect!";
				return auth;
			}
			var userrole = await userManager.GetRolesAsync(user);

			if (user.RefreshTokens.Any(t => t.IsActive))
			{
				var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
				auth.RefreshToken = activeRefreshToken.Token;
				auth.RefreshTokenExpiration = activeRefreshToken.ExpiresOn.ToLocalTime();
			}
			else
			{
				var refreshToken = await GenerateRefreshToken();
				auth.RefreshToken = refreshToken.Token;
				auth.RefreshTokenExpiration = refreshToken.ExpiresOn;
				user.RefreshTokens.Add(refreshToken);
				await userManager.UpdateAsync(user);
			}
			var token = await CreateToken(user);

			auth.Email = user.Email;
			auth.ExpiresOn = token.ValidTo.ToLocalTime();
			auth.IsAuthenticated = true;
			auth.Role = userrole.First().ToString();
			auth.Token = new JwtSecurityTokenHandler().WriteToken(token);
			auth.FirstName = user.FirstName;
			auth.LastName = user.LastName;
			auth.Gender = user.Gender.ToString();
			auth.ImageUrl = user.ImageUrl;

			return auth;
		}
		public async Task<AuthModel> RefreshTokenAsync(string token)
		{
			var auth = new AuthModel();
			var user = await userManager.Users.SingleOrDefaultAsync(r => r.RefreshTokens.Any(t => t.Token == token));
			if (user is null)
			{
				auth.Message = "Invalid Token";
				return auth;
			}
			var refreshtoken = user.RefreshTokens.Single(s => s.Token == token);
			if (!refreshtoken.IsActive)
			{
				auth.Message = "InActive Token";
				return auth;
			}
			var newrefreshtoken = await GenerateRefreshToken();
			user.RefreshTokens.Add(newrefreshtoken);
			await userManager.UpdateAsync(user);

			var jwtToken = await CreateToken(user);
			auth.ExpiresOn = jwtToken.ValidTo.ToLocalTime();
			auth.IsAuthenticated = true;
			auth.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			auth.Email = user.Email;
			var roles = await userManager.GetRolesAsync(user);
			auth.Role = roles.First().ToString();
			auth.RefreshToken = newrefreshtoken.Token;
			auth.RefreshTokenExpiration = newrefreshtoken.ExpiresOn.ToLocalTime();
			auth.FirstName = user.FirstName;
			auth.LastName = user.LastName;
			auth.Gender = user.Gender.ToString();
			auth.ImageUrl = user.ImageUrl;

			return auth;

		}
		private async Task<RefreshToken> GenerateRefreshToken()
		{
			var randomNumber = new byte[32];

			using var generator =  new RNGCryptoServiceProvider();

			 generator.GetBytes(randomNumber);

			return new RefreshToken
			{
				Token = Convert.ToBase64String(randomNumber),
				ExpiresOn = DateTime.UtcNow.AddDays(10).ToLocalTime(),
				CreatedOn = DateTime.UtcNow.ToLocalTime()
			};
		}
		public async Task<AuthModel> Register(Register register)
		{
			var user = await userRepo.AddCustomer(register);
			if (user.ok == false)
			{
				return new AuthModel
				{
					IsAuthenticated = false,
					Message = user.Message
				};
			}
			var token = await CreateToken(user.User);
			var userrole = await userManager.GetRolesAsync(user.User);
			var refreshToken =await GenerateRefreshToken();
			user.User.RefreshTokens?.Add(refreshToken);
			await userManager.UpdateAsync(user.User);

			return new AuthModel
			{
				Email = user.User.Email,
				ExpiresOn = token.ValidTo,
				IsAuthenticated = true,
				Role = userrole.First().ToString(),
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				FirstName = user.User.FirstName,
				LastName = user.User.LastName,
				Gender = user.User.Gender.ToString(),
				RefreshToken = refreshToken.Token,
				RefreshTokenExpiration = refreshToken.ExpiresOn.ToLocalTime(),
				ImageUrl=user.User.ImageUrl
			};
		}
		private async Task<JwtSecurityToken> CreateToken(User user)
		{
			var userclaim = await userManager.GetClaimsAsync(user);
			var userrole = await userManager.GetRolesAsync(user);
			var roleclaims = new List<System.Security.Claims.Claim>();
			foreach (var claim in userrole)
			{
				roleclaims.Add(new System.Security.Claims.Claim("role", claim));
			}
			var Claims = new[]
			{
		new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim("FirstName",user.FirstName),
				new Claim("LastName",user.LastName),
				new Claim(JwtRegisteredClaimNames.Gender,user.Gender.ToString())
			}
			.Union(roleclaims)
			.Union(userclaim);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: jwt.Issuer,
				audience: jwt.Audience,
				claims: Claims,
				signingCredentials: signingCredentials,
				expires: DateTime.UtcNow.AddMinutes(jwt.DurationInMinutes).ToLocalTime()
				);


			return token;
		}
	}
}
