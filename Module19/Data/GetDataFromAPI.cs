using Module19.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebClient_PeopleContacts.Data
{
    public class GetDataFromAPI : IGetData
    {

        HttpClient httpClient;
        string url;
        string json;

        public GetDataFromAPI()
        {
            httpClient = new HttpClient();
        }
        public IEnumerable<Person> GetAll()
        {
            url = @"https://localhost:7173/getPersons";


            json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);
            //return new List<Person>();

            
        }


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


        public Person GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
