using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Admin.Controllers
{
    public class AdminPacientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
