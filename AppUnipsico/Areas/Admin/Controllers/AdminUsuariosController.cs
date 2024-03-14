using AppUnipsico.Models;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminUsuariosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        public AdminUsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users
                .OrderBy(u => u.UserName)
                .ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = registerViewModel.Email,
                    NomeCompleto = registerViewModel.NomeCompleto,
                    Cpf = registerViewModel.Cpf,
                    DataNascimento = registerViewModel.DataNascimento,
                    DataRegistro = DateTime.Now,
                    Email = registerViewModel.Email,
                };

                var result = await _userManager.CreateAsync(usuario, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, registerViewModel.SelectedRole);

                    return RedirectToAction("Index", new { area = "Admin" });

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerViewModel);
        }

    }
}
