using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminDatasController : Controller
    {
        private readonly DatasRepository _datasRepository;

        public AdminDatasController(DatasRepository datasRepository)
        {
            _datasRepository = datasRepository;
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            var datas = await _datasRepository.BuscaDatasSemTracking();

            if (datas is not null && datas.Any())
            {
                int pageIndex = page ?? 1;
                var paginatedData = Paginacao<Datas>.CreateAsync(datas, pageIndex, pageSize);

                return View(paginatedData);
            }

            return View(new Paginacao<Datas>(new List<Datas>(), 0, 0, pageSize));
        }

        public async Task<IActionResult> DeletarData(Guid dataId)
        {
            if (string.IsNullOrEmpty(dataId.ToString()))
            {
                return NotFound();
            }

            var data = await _datasRepository.BuscaDataPorId(dataId);

            if (data is null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid dataId)
        {
            if (string.IsNullOrEmpty(dataId.ToString()))
            {
                return NotFound();
            }

            var data = await _datasRepository.BuscaDataPorId(dataId);

            if (data is null)
            {
                return NotFound();
            }

            await _datasRepository.DeletarData(data);

            return RedirectToAction("Index");
        }
    }
}
