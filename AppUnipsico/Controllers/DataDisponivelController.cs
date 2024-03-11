using AppUnipsico.Services.Impl;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    public class DataDisponivelController : Controller
    {
        private readonly DataDisponivelService _dataDisponivelService;

        public DataDisponivelController(DataDisponivelService dataDisponivelService)
        {
            _dataDisponivelService = dataDisponivelService;
        }

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
