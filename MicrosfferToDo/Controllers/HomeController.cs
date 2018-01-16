using System.Web.Mvc;

namespace MicrosfferToDo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Microsffer ToDo";

            return View();
        }
    }
}
