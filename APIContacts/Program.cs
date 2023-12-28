using APIContacts.DB;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

string myConnection = builder.Configuration.GetConnectionString("MSSQLConnection");
services.AddDbContext<ContextDB>(options => options.UseSqlServer(myConnection), ServiceLifetime.Scoped);
services.AddControllers();

var app = builder.Build();


app.MapGet("/", () => "/GetPersons - получить список всех контактов \n/GetById/Id получить контакт по Id\n/DeleteById/Id  удалить контакт по Id (Delete запрос)\n/AddPerson добавить контакт (ожидает Post объекта Person без Id)\n/ChangePerson изменить контакт (ожидает Put объекта Person с Id)");

app.MapGet("/GetPersons", GetPersons);
app.MapGet("/GetById/{Id}", GetById);
app.MapDelete("/DeleteById/{Id}", DeleteById);
app.MapPost("/AddPerson", AddPerson);
app.MapPut("/ChangePerson", ChangePerson);


app.Run();

List<Person> GetPersons( ContextDB db)
{
    return db.Persons.ToList();
}
Person  GetById(ContextDB db,int Id)
{
    return db.Persons.First(x=>x.Id==Id);
}

async Task<List<Person>> DeleteById(ContextDB db, int Id)
{
    db.Persons.Remove(await db.Persons.FirstAsync(x => x.Id == Id));
    await db.SaveChangesAsync();
    return db.Persons.ToList();
}

async Task<List<Person>> AddPerson(ContextDB db,Person person)
{
    person.Id = 0;
    await db.Persons.AddAsync(person);
    await db.SaveChangesAsync();
    return db.Persons.ToList();
}

async Task<List<Person>> ChangePerson(ContextDB db,Person person)
{
    Person oldPerson = db.Persons.First(x => x.Id == person.Id);
    oldPerson.LastName = person.LastName;
    oldPerson.FirstName = person.FirstName;
    oldPerson.SurName = person.SurName;
    oldPerson.PhoneNumber = person.PhoneNumber;
    oldPerson.PostalAddress = person.PostalAddress;
    oldPerson.Description = person.Description;
    
    await db.SaveChangesAsync();
    return db.Persons.ToList();
}
