using Microsoft.AspNetCore.Mvc;

namespace Remote.Controllers
{
    public class HomeController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("/index.html");
        }
    }
}
