
using System.Text.RegularExpressions;

///�������:
///2. Logout ��� ������ ����� ��������� ����. ����� �������� ���� � ����� �����, ���� �������� �����������.
///3.Only the invariant culture is supported in globalization-invariant mode. See https://aka.ms/GlobalizationInvariantMode for more information. (Parameter 'name')en - us is an invalid culture identifier.





using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module19.Model;
using Module19.Authorization;
using WebClient_PeopleContacts.Data;


var builder = WebApplication.CreateBuilder(args);
string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");
string authString = builder.Configuration.GetConnectionString("AuthLConnection");
builder.Logging.AddDebug();
IServiceCollection services = builder.Services;
//LoggerFactory.Create(b=>b.AddDebug());
//services.AddLogging(b=>b.AddDebug());

//services.AddDbContext<PeopleDBContext>(options => options.UseSqlServer(myConnection),ServiceLifetime.Scoped);
services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authString), ServiceLifetime.Scoped);

services.AddTransient<IGetData,GetDataFromAPIControllers>();//���������� ������� ��������� ������ (��������� �� ��� API )

services.AddMvc(options => options.EnableEndpointRouting = false);
IdentityBuilder iB = services.AddIdentity<User, IdentityRole>();

iB.AddEntityFrameworkStores<AuthDbContext>();
iB.AddDefaultTokenProviders();
iB.AddRoles<IdentityRole>();



services.Configure<IdentityOptions>(options =>
{
    //������������ ���������� � ������
    var pass = options.Password;
    pass.RequiredLength = 2; // ����������� ���������� ������ � ������
    pass.RequireDigit = false;
    pass.RequireUppercase = false;
    pass.RequireNonAlphanumeric = false;
    pass.RequireLowercase = false;
    
        
    //options.Lockout.MaxFailedAccessAttempts = 1; // ���������� ������� � ����������
    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    
});

services.ConfigureApplicationCookie(options =>
{
    // ������������ Cookie � ����� ������������� �� ��� �������� �����������
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

