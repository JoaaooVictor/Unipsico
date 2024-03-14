using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminProfessorRole")]
    public class AdminDatasController : Controller
    {
        private readonly AppUnipsicoDb _context;

        public AdminDatasController(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _context.Datas.ToListAsync();

            if (datas is not null)
            {
                var datasViewModel = new DatasViewModel
                {
                    Datas = datas,
                    Mensagem = "Datas inclusas no banco de dados!"
                };

                return View(datasViewModel);
            }

            return View(new DatasViewModel { Datas = null, Mensagem = "Nenhum data encontrada no banco de dados!" });
        }
    }
}
