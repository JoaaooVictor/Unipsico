using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminUsuariosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly AppUnipsicoDb _context;
        public AdminUsuariosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, AppUnipsicoDb context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public IActionResult Index()
        {
            var usuarios = _userManager.Users
                .OrderBy(u => u.UserName)
                .ToList();

            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> DetalhesUsuario(string usuarioId)
        {
            if (usuarioId is null)
            {
                return BadRequest();
            }
            else
            {
                var usuario = await _userManager.FindByIdAsync(usuarioId);

                if (usuario is not null)
                {
                    var usuarioConsultas = await _context.Consultas
                        .Where(c => c.UsuarioId == usuario.Id)
                        .Include(c => c.Usuario)
                        .Include(c => c.DataConsulta)
                        .OrderBy(c => c.DataConsulta.Data)
                        .ToListAsync();

                    if (usuarioConsultas is not null && usuarioConsultas.Count > 0)
                    {
                        var usuariodetalheViewModel = new UsuarioDetalheViewModel
                        {
                            Consultas = usuarioConsultas,
                            Usuario = usuario,
                            Mensagem = "",
                        };

                        return View(usuariodetalheViewModel);
                    }

                    return View(new UsuarioDetalheViewModel { Usuario = usuario, Consultas = null, Mensagem = $"Nenhuma consulta encontrada para o usuário {usuario.NomeCompleto}!" });
                }
                else
                {
                    return NotFound();
                }
            }
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

        public async Task<IActionResult> DeletarUsuario(string usuarioId)
        {
            if (usuarioId is null)
            {
                return NotFound();
            }

            var usuario = await _userManager.Users
                .Where(u => u.Id == usuarioId)
                .Include(u => u.Consultas)
                .ThenInclude(c => c.DataConsulta)
                .FirstOrDefaultAsync();

            if (usuario is null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string usuarioId)
        {

            if (usuarioId is null)
            {
                return BadRequest();
            }

            var usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario is null)
            {
                return NotFound();
            }

            var resultUserManager = await _userManager.DeleteAsync(usuario);

            if (resultUserManager.Succeeded)
            {
                return RedirectToAction("Index", new { area = "Admin" });
            }
            else
            {
                foreach (var error in resultUserManager.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View("Error");
            }
        }
    }
}
