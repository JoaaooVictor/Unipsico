using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class ConsultaController : Controller
    {
        public IActionResult Agendar(Guid DataConsultaId)
        {
            return View();
        }
    }
}
