
///Вопросы:
///1. Делаю LogIn, потом LogOut и снова LogIn в тот же аккаунт - вылетает ошибка InvalidOperationException: A second operation was started on this context instance before a previous operation completed. This is usually caused by different threads concurrently using the same instance of DbContext. For more information on how to avoid threading issues with DbContext, see https://go.microsoft.com/fwlink/?linkid=2097913. Не понимаю, как победить. 
///2. Logout как делать перед закрытием окна. Когда закрываю окно и снова вхожу, юзер остается авторизован.






using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module19.Model;
using Module19.Authorization;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


var builder = WebApplication.CreateBuilder(args);
string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");
string authString = builder.Configuration.GetConnectionString("AuthLConnection");
builder.Logging.AddDebug();
IServiceCollection services = builder.Services;
//LoggerFactory.Create(b=>b.AddDebug());
//services.AddLogging(b=>b.AddDebug());

services.AddDbContext<PeopleDBContext>(options => options.UseSqlServer(myConnection),ServiceLifetime.Scoped);
services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authString), ServiceLifetime.Scoped);

services.AddMvc(options => options.EnableEndpointRouting = false);
IdentityBuilder iB = services.AddIdentity<User, IdentityRole>();

iB.AddEntityFrameworkStores<AuthDbContext>();
iB.AddDefaultTokenProviders();
iB.AddRoles<IdentityRole>();



services.Configure<IdentityOptions>(options =>
{
    //Конфигурация Требований к данным
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
    options.AccessDeniedPath = "/Account/AccessDenied";
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

