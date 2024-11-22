
using E_ecommerce.Data.Entites;
using E_ecommerce.Infrastructure.Context;
using E_ecommerce.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_ecommerce.Data.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using E_ecommerce.Service;
using E_ecommerce.Service.Seed;
using E_ecommerce.Core;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace E_ecommerce.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
				//c.EnableAnnotations();

				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
												{
												{
												new OpenApiSecurityScheme
												{
																Reference = new OpenApiReference
																{
																				Type = ReferenceType.SecurityScheme,
																				Id = JwtBearerDefaults.AuthenticationScheme
																}
												},
												Array.Empty<string>()
												}
											});
			});
			//-----------------------
			builder.Services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<ApplicationContext>();
			builder.Services.AddDbContext<ApplicationContext>(op =>
			op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
				);

			//-----------------------
			builder.Services.Add_Infrastructure_Dependencies();
			builder.Services.Add_Module_Service_Dependencies();
			builder.Services.Add_Module_Core_Dependencies();
			//-----------------------
			builder.Services.Configure<JWTModel>(builder.Configuration.GetSection("JWT"));
			//-----------------------
			builder.Services.AddAuthentication(options =>
			{

				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}
				).AddJwtBearer(options =>
				{
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidIssuer = builder.Configuration["JWT:Issuer"],
						ValidAudience = builder.Configuration["JWT:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
						ClockSkew = TimeSpan.Zero
					};

				})
				;
			//-----------------------
			builder.Services.AddCors(o =>
			o.AddDefaultPolicy(
				p =>
				{
					p.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					;
				}
				)
				);
			//-----------------------
			builder.Services.AddLocalization(opt =>
			{
				opt.ResourcesPath = "";
			}); 
			builder.Services.Configure<RequestLocalizationOptions>(op =>

			{
				List<CultureInfo> cultureInfos = new List<CultureInfo>()
			{
				new CultureInfo( "en-US" ),
				new CultureInfo( "ar-EG" )
			};
				op.DefaultRequestCulture = new RequestCulture("en-US");
				op.SupportedCultures = cultureInfos;
				op.SupportedUICultures = cultureInfos;
			}
			);
			//-----------------------
			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			var lo = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(lo.Value);
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseCors();
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			SeedRolesAndAdmin.InitializeAsync(app.Services).Wait();
			app.Run();
		}
	}
}
