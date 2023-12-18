using LoginRegisterIdentity.Data;
using LoginRegisterIdentity.Interfaces;
using LoginRegisterIdentity.Models;
using LoginRegisterIdentity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using LoginRegisterIdentity.Helpers;
using Microsoft.Extensions.Options;

namespace LoginRegisterIdentity
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("default");

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
			builder.Services.AddScoped<IProductRepository, ProductRepository>();
			builder.Services.AddScoped<IPhotoService, AzureRepository>();
			builder.Services.AddDbContext<AppDbContext>(
				options => options.UseSqlServer(connectionString)
			);
			builder.Services.AddIdentity<AppUser, IdentityRole>(
				options =>
				{
					options.Password.RequiredUniqueChars = 0;
					options.Password.RequireUppercase = false;
					options.Password.RequiredLength = 5;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireLowercase = false;
				})
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			// Configure Azure Storage Options
			builder.Services.Configure<AzureStorageOptions>(builder.Configuration.GetSection("AzureStorageOptions"));
			builder.Services.AddSingleton(x =>
			{
				var options = x.GetRequiredService<IOptions<AzureStorageOptions>>().Value;
				return new BlobServiceClient(options.ConnectionString);
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
