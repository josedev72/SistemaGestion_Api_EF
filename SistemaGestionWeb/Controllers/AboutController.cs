using Microsoft.AspNetCore.Mvc;

namespace SistemaGestionWeb.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
