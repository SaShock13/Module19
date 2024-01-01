
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GetDataLibrary
{
    public class GetDataFromAPIControllers : IGetData
    {
        HttpClient httpClient;
        string url = @"https://localhost:7112/api/People" ;
        string json;

        public GetDataFromAPIControllers()
        {
            httpClient = new HttpClient();
        }
        public IEnumerable<Person> GetAll()
        {
            json = httpClient.GetStringAsync(url).Result;

            
            List<Person> result = JsonConvert.DeserializeObject<IEnumerable<Person>>(json).ToList();
            
            return result;
        }

        public void AddPerson(Person person)
        {

           var result = httpClient.PostAsJsonAsync(url, person).Result;

        }

        public void ChangePerson(Person person)
        {

            var result = httpClient.PutAsJsonAsync(url, person).Result;
            
        }

        public void DeleteById(int id)
        {
            var result = httpClient.DeleteAsync(url + $"/{id}").Result;
        }


        public Person GetById(int id)
        {
            json = httpClient.GetStringAsync(url + $"/{id}").Result;
            return JsonConvert.DeserializeObject<Person>(json);
        }
    }
}
