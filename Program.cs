using Database;
using identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<IdentityContext>();



builder.Services.AddDbContext<IdentityContext>(idenity =>
{
    idenity.UseSqlServer(builder.Configuration.GetConnectionString("ShopDatabase"));
});

builder.Services.AddDbContext<ShopContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("ShopDatabase"));
});


builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/account/login";
    option.LogoutPath = "/account/logout";
    option.Cookie.Name = "GoodDayShopUserCookie";
});

builder.Services.AddControllersWithViews();




var app = builder.Build();


app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );
    });

app.MapControllerRoute(
    name: "Main",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);






app.Run();
