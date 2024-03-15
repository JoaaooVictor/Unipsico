using AppUnipsico.Areas.Admin.ViewModels;
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
    public class AdminEstagiosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AppUnipsicoDb _context;

        public AdminEstagiosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AppUnipsicoDb context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var estagios = await _context.Estagios
                                        .Include(e => e.Aluno)
                                        .Include(e => e.Instituicao)
                                        .ToListAsync();
            return View(estagios);
        }

        [HttpGet]
        public IActionResult CriarInstituicao()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarInstituicao(InstituicaoViewModel instituicaoViewModel)
        {
            return View();
        }

    }
}
