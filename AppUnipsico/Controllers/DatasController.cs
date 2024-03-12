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

        public async Task<IActionResult> Index()
        {
            var dataDisponivelViewModel = new DataDisponivelViewModel()
            {
                DatasDisponiveis = await _dataDisponivelService.DatasDisponiveis(),
            };

            return View(dataDisponivelViewModel);
        }
    }
}
