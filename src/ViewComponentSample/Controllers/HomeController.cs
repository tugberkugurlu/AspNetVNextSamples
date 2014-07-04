using Microsoft.AspNet.Mvc;

namespace ViewComponentSample.Controllers
{
    public class HomeController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}