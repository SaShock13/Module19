using APIContacts.DB;
using APIwithControllers.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");


services.AddDbContext<ContextDB>(options => options.UseSqlServer(myConnection), ServiceLifetime.Scoped);
services.AddControllers();

//services.AddControllers<PeopleController>();
var app = builder.Build();
app.MapControllers();
app.MapGet("/", () => "GET api/People - список всех контактов \n" +
                        "GET api/People/id - вывести контакт по ID \n" +
                        "POST api/People - ƒобавить контакт (ожидает обьект Person)\n" +
                        "PUT api/People/id - заменить контакт (ожидает обьект Person)\n" +
                        "DELETE api/People/id - удалить контакт по ID \n" +
                        " ");

app.Run();
