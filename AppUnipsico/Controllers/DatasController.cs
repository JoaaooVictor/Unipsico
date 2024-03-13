using AppUnipsico.Services.Impl;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class DatasController : Controller
    {
        private readonly DataService _dataDisponivelService;

        public DatasController(DataService dataDisponivelService)
        {
            _dataDisponivelService = dataDisponivelService;
        }

        [HttpGet]   
        public IActionResult Index()
        {
            var dataDisponivelViewModel = new DataViewModel()
            {
                DatasDisponiveis = _dataDisponivelService.DatasDisponiveis(),
            };

            return View(dataDisponivelViewModel);
        }
    }
}
