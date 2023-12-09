using Microsoft.AspNetCore.Mvc;
using Module19.Model;


namespace Module19.Controllers
{
    public class PeopleController : Controller
    {
        List<Person> Persons { get; set; }
        PeopleDBContext dataBase;
        public PeopleController(PeopleDBContext db)
        {

            dataBase = db;
            Persons = db.Persons.ToList();
            //FillDb();
        }


        public IActionResult Index()
        {
            return View(Persons);
        }
        public IActionResult FullInfo(int id)
        {
            ViewBag.Title = "Полная Информация";
            Person person = Persons.Where(x => x.Id == id).First();
            return View(person);
        }

        public void FillDb()
        {
            var person1 = new Person()
            {
                
                FirstName = "Иван",
                LastName = "Смирнов",
                SurName = "Петрович",
                PhoneNumber = "+7 (987) 654-32-10",
                PostalAddress = "Санкт-Петербург",
                Description = "Иван Петрович - тихий и упорядоченный человек с прекрасным чувством юмора. Он всегда готов помочь окружающим и часто выступает в качестве посредника в конфликтных ситуациях."
            };
            var person2 = new Person()
            {
                
                FirstName = "Александра",
                LastName = "Козлова",
                SurName = "Ивановна ",
                PhoneNumber = "654-32-11",
                PostalAddress = "Москва",
                Description = "Александра Ивановна - энергичная и целеустремленная женщина. Она обладает острым умом и всегда готова бросить вызов сложностям. Благодаря своейцелеустремленности, Александра Ивановна часто становится вдохновением для других людей."
            };
            var person3 = new Person()
            {
                
                FirstName = "Максим",
                LastName = "Попов",
                SurName = "Сергеевич",
                PhoneNumber = "654-32-12",
                PostalAddress = "Воронеж",
                Description = "Максим Сергеевич - общительный и дружелюбный парень. Он всегда готов поддержать и помочь своим друзьям. Максим Сергеевич заботится о других и проявляет искренность во всех отношениях."
            };
            var person4 = new Person()
            {
                
                FirstName = "Елена",
                LastName = "Соколова",
                SurName = "Алексеевна ",
                PhoneNumber = "54-32-13",
                PostalAddress = "Жигулёвск",
                Description = "Елена Алексеевна - спокойная и терпеливая женщина. Она обладает сильной волей и умением преодолевать трудности. Елена Алексеевна всегда готова выслушать других и предложить свою помощь."
            };
            var person5 = new Person()
            {
                
                FirstName = "Дмитрий ",
                LastName = "Иванов",
                SurName = "Николаевич ",
                PhoneNumber = "654-32-14",
                PostalAddress = "Колыма",
                Description = "Дмитрий Николаевич - творческая и инициативная личность. Он обладает ярким воображением и нестандартным подходом к решению задач. Дмитрий Николаевич часто выступает в качестве инициатора новых проектов и старается вносить позитивные изменения вокруг."
            };
            List<Person> list = new List<Person>() { person1, person2, person3, person4, person5 };
            dataBase.Persons.AddRange(list);
            
            dataBase.SaveChangesAsync();

        }
    }
}
