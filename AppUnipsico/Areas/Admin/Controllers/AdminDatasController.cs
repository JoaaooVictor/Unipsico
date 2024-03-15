using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Data.Context;
using AppUnipsico.Models;
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

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            var datas = await _context.Datas.AsNoTracking().OrderBy(c => c.Data).ToListAsync();

            if (datas is not null && datas.Any())
            {
                int pageIndex = page ?? 1;
                var paginatedData = Paginacao<Datas>.CreateAsync(datas, pageIndex, pageSize);

                return View(paginatedData);
            }

            return View(new Paginacao<Datas>(new List<Datas>(), 0, 0, pageSize));
        }


    }
}
