using Microsoft.AspNetCore.Mvc;

namespace Geo_Back.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
