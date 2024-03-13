using AppUnipsico.Enums;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminAlunosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AdminAlunosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users
                .Where(u => u.TipoUsuario == TipoUsuarioEnum.Aluno)
                .OrderBy(u => u.UserName)
                .ToList();

            return View(users);
        }

    }
}
