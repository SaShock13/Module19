using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using GetDataLibrary;


namespace WPFClient_PeopleContacts.Models
{
    class GetDataFromApi
    {

        HttpClient httpClient;
        string url = @"https://localhost:7112/api/People";
        string json;

        public GetDataFromApi()
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

            httpClient.PutAsJsonAsync(url, person);

        }

        public void DeleteById(int id)
        {
            httpClient.DeleteAsync(url + $"/{id}");
        }


        public Person GetById(int id)
        {
            json = httpClient.GetStringAsync(url + $"/{id}").Result;
            return JsonConvert.DeserializeObject<Person>(json);
        }
    }
}
