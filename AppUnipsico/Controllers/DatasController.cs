using AppUnipsico.Services.Impl;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class DatasController : Controller
    {
        private readonly DataService _datasServices;

        public DatasController(DataService dataDisponivelService)
        {
            _datasServices = dataDisponivelService;
        }

        [HttpGet]   
        public IActionResult Index()
        {
            var dataDisponivelViewModel = new DataViewModel()
            {
                DatasDisponiveis = _datasServices.DatasDisponiveis(),
            };

            return View(dataDisponivelViewModel);
        }
    }
}
