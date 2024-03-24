using AppUnipsico.Areas.Aluno.Repositories;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Policy = "RequireAlunoRole")]
    public class AlunoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly AlunoRepository _alunoRepository;

        public AlunoController(UserManager<Usuario> userManager, AlunoRepository alunoRepository)
        {
            _userManager = userManager;
            _alunoRepository = alunoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Estagios()
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario is null)
            {
                return NotFound();
            }

            var estagios = await _alunoRepository.BuscaTodosEstagiosPorAluno(usuario.Id);

            return View(estagios);
        }

        public IActionResult Presencas()
        {
            return View();
        }
    }
}
