using Microsoft.EntityFrameworkCore;
using Module19.Controllers;
using Module19.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


var builder = WebApplication.CreateBuilder(args);
string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");

builder.Services.AddDbContext<PeopleDBContext>(options => options.UseSqlServer(myConnection));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

var app = builder.Build();
app.UseMvc(r =>
{
    r.MapRoute(
    name: "default",
    template: "{controller=People}/{action=Index}/{id?}");
}
    );

app.Run();
