
namespace GetDataLibrary
{
    public class GetDataDB : IGetData
    {
        public void AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void ChangePerson(Person person)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            PeopleDBContext db = new PeopleDBContext();
           
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
                //// создаем два объекта User
                //User user1 = new User { Name = "Tom", Age = 33 };
                //User user2 = new User { Name = "Alice", Age = 26 };

                //// добавляем их в бд
                //db.Users.AddRange(user1, user2);
                //db.SaveChanges();
                return db.Persons;
            
           // PeopleDBContext db= new PeopleDBContext() ;
           
            
        }

        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
