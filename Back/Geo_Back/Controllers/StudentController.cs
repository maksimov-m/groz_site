using Microsoft.AspNetCore.Mvc;

namespace Geo_Back.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
