using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using Microsoft.AspNetCore.Identity;
using BerrasBio.Models;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<BerrasBioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BerrasBioContext")));

builder.Services.AddAutoMapper(typeof(Program));
// Modify register settings
builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 10;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
})
 .AddEntityFrameworkStores<BerrasBioContext>();

// Add services to the container.


builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Start}/{action=Index}/{id?}");

app.Run();

