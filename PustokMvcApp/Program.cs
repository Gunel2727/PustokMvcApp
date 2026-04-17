using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PustokMvcApp.Data;
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
var app = builder.Build();



// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
      );



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
