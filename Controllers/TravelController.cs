using Microsoft.AspNetCore.Mvc;

namespace MSIT147Site.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
