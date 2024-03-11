using AppUnipsico.Services.Interfaces;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
