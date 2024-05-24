using Maxim.DAL;
using Maxim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(opt =>
			opt.UseSqlServer(builder.Configuration.GetConnectionString("default")));
			builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
			{
			opt.User.RequireUniqueEmail = true;
			opt.Password.RequiredLength = 3;
			opt.Password.RequireNonAlphanumeric = false;
			opt.Password.RequireDigit = false;
			opt.Password.RequireLowercase = false;
			opt.Password.RequireUppercase = false;
		}).AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();
var app = builder.Build();
			app.UseStaticFiles();

		app.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute("default", "{Controller=Home}/{action=index}/{id?}" );

			app.Run();
