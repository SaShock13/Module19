using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Module19.Model;
using WebClient_PeopleContacts.Data;


namespace Module19.Controllers
{
    public class PeopleController : Controller
    {
        List<Person> Persons { get; set; }
        IGetData dataBase;
        ILogger<PeopleController> logger;
        public PeopleController(ILoggerFactory loggerFactory,ILogger<PeopleController> logger2,IGetData db)
        {
            
            
            dataBase = db;
            
            logger = loggerFactory.CreateLogger<PeopleController>();
            logger.LogCritical("Это через логфэктори!!!!");

            logger2.LogCritical("Это через логгер");
            
        }

        

        public IActionResult Index()
        {
            logger.LogCritical("Index сработал в PeopleController");

            return View(dataBase.GetAll());
            
        }
        public IActionResult FullInfo(int id)
        {

            
            ViewBag.Title = "Полная Информация";
           
            return View(dataBase.GetById(id));
        }
        [Authorize(Roles = "admin")]
        public IActionResult DeletePerson(int id)
        {
            dataBase.DeleteById(id);
            return Redirect("~/");

        }


        [HttpGet]
        [Authorize]
        public IActionResult AddPerson()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            dataBase.AddPerson(person);
            
            return Redirect("~/");

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult EditPerson(int id)
        {
            
            ViewBag.Title = "Полная Информация";
            
            IEnumerable<Person> persons = dataBase.GetAll();
            Person person = persons.Where(x => x.Id == id).First();

            return View(person);
        }

        [HttpPost]
        
        public IActionResult EditPerson(Person person)
        {

            dataBase.ChangePerson(person);
           
            return Redirect("~/");

        }
       
    }
}
