using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminInstituicoesController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AppUnipsicoDb _context;

        public AdminInstituicoesController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AppUnipsicoDb context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var instituicoes = await _context.Instituicoes
                                                .Include(i => i.Estagios)
                                                .ToListAsync();

            return View(instituicoes);
        }
    }
}
