using AppUnipsico.Areas.Admin.Repositories;
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
        private readonly UsuarioRepository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        public AdminUsuariosController(UserManager<Usuario> userManager, UsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioRepository.BuscaTodosUsuarios();
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
                return NotFound();
            }
            else
            {
                var usuario = await _usuarioRepository.BuscaUsuarioPorId(usuarioId);

                if (usuario is not null)
                {
                    var usuarioConsultas = await _usuarioRepository.BuscaConsultasPorUsuario(usuario);

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
        public async Task<IActionResult> Register(RegistraUsuarioViewModel registraUsuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuarioCriado = await _usuarioRepository.CriaUsuario(registraUsuarioViewModel);

                if (usuarioCriado.Succeeded)
                {
                    return RedirectToAction("Index", new { area = "Admin" });
                }

                foreach (var error in usuarioCriado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registraUsuarioViewModel);
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
