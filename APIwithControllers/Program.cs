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
app.MapGet("/", () => "GET api/People - ������ ���� ��������� \n" +
                        "GET api/People/id - ������� ������� �� ID \n" +
                        "POST api/People - �������� ������� (������� ������ Person)\n" +
                        "PUT api/People/id - �������� ������� (������� ������ Person)\n" +
                        "DELETE api/People/id - ������� ������� �� ID \n" +
                        " ");

app.Run();
