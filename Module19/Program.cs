 using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module19.Model;
using Module19.Authorization;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


var builder = WebApplication.CreateBuilder(args);
string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");
string authString = builder.Configuration.GetConnectionString("AuthLConnection");

IServiceCollection services = builder.Services;


services.AddDbContext<PeopleDBContext>(options => options.UseSqlServer(myConnection));
services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authString));

services.AddMvc(options => options.EnableEndpointRouting = false);
 IdentityBuilder iB = services.AddIdentity<User, IdentityRole>();
iB.AddEntityFrameworkStores<AuthDbContext>();
iB.AddDefaultTokenProviders();
iB.AddRoles<IdentityRole>();

services.Configure<IdentityOptions>(options =>
{
    var pass = options.Password;
    pass.RequiredLength = 2; // минимальное количество знаков в пароле
    pass.RequireDigit = false;
    pass.RequireUppercase = false;
    pass.RequireNonAlphanumeric = false;
    pass.RequireLowercase = false;
    
        
    //options.Lockout.MaxFailedAccessAttempts = 1; // количество попыток о блокировки
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    
});

services.ConfigureApplicationCookie(options =>
{
    // конфигурация Cookie с целью использования их для хранения авторизации
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.SlidingExpiration = true;
});


var app = builder.Build();
app.UseMvc(r =>
{
    r.MapRoute(
    name: "default",
    template: "{controller=People}/{action=Index}/{id?}");
}
    );

app.Run();

