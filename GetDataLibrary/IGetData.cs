

namespace GetDataLibrary
{
    public interface IGetData
    {

        IEnumerable<Person> GetAll();
        Person GetById(int id);
        void AddPerson(Person person);

        void DeleteById(int id);
        void ChangePerson(Person person);

    }
}
