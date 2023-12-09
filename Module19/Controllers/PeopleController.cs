using Microsoft.AspNetCore.Mvc;
using Module19.Model;

namespace Module19.Controllers
{
    public class PeopleController : Controller
    {
        Repository repository = new();

        public IActionResult Index()
        {
            return View(repository.Persons);
        }
    }
}
