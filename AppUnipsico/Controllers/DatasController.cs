using AppUnipsico.Services.Impl;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class DatasController : Controller
    {
        private readonly DataDisponivelService _dataDisponivelService;

        public DatasController(DataDisponivelService dataDisponivelService)
        {
            _dataDisponivelService = dataDisponivelService;
        }

        [HttpGet]   
        public IActionResult Index()
        {
            var dataDisponivelViewModel = new DataDisponivelViewModel()
            {
                DatasDisponiveis = _dataDisponivelService.DatasDisponiveis(),
            };

            return View(dataDisponivelViewModel);
        }
    }
}
