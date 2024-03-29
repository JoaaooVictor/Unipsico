using AppUnipsico.Models;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AppUnipsico.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AccountController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin", new {area = "Admin"});
                    }
                    else if (await _userManager.IsInRoleAsync(user, "Aluno"))
                    {
                        return RedirectToAction("Index", "Aluno", new { area = "Aluno" });
                    }
                    else if(string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ConsultaPaciente", "Consulta");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Senha incorreta, tente novamente");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "E-mail não está cadastrado");
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                var user = new Usuario
                {
                    UserName = registerViewModel.Email,
                    NomeCompleto = registerViewModel.NomeCompleto,
                    Cpf = registerViewModel.Cpf,
                    DataNascimento = registerViewModel.DataNascimento,
                    DataRegistro = DateTime.Now,
                    Email = registerViewModel.Email,
                    TipoUsuario = Enums.TipoUsuarioEnum.Paciente,
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Paciente");
                    RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("Registro", "Falha ao registrar usuário!");
                }
            }

            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
