using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp;
using PustokMvcApp.Data;
using PustokMvcApp.Models;
using PustokMvcApp.Services;
using PustokMvcApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BankService>();
builder.Services.AddScoped<LayoutService>();
builder.Services.Configure<GroupInfoSettings>(builder.Configuration.GetSection("Group"));

var config= builder.Configuration;

builder.Services.AddDbContext<PustokAppDbContext>(options =>
options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt=>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireLowercase = true;

    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.AllowedForNewUsers = true;
})
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<PustokAppDbContext>();
   

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
app.UseStaticFiles();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
      );



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

Console.WriteLine("Hello, World!");
