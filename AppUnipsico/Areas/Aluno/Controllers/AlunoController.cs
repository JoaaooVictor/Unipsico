using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Areas.Aluno.ViewModels;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Policy = "RequireAlunoRole")]
    public class AlunoController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly AppUnipsico.Areas.Aluno.Repositories.AlunoRepository _alunoRepository;
        private readonly InstituicoesRepository _instituicoesRepository;
        private readonly EstagioRepository _estagioRepository;
        public AlunoController(UserManager<Usuario> userManager, AppUnipsico.Areas.Aluno.Repositories.AlunoRepository alunoRepository, InstituicoesRepository instituicoesRepository, EstagioRepository estagioRepository)
        {
            _userManager = userManager;
            _alunoRepository = alunoRepository;
            _instituicoesRepository = instituicoesRepository;
            _estagioRepository = estagioRepository;
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

        [HttpGet]
        public async Task<IActionResult> SolicitarEstagio()
        {
            ViewBag.Instituicoes = await _instituicoesRepository.BuscarInstituicoes();
            var aluno = await _userManager.GetUserAsync(User);

            var alunoEstagioViewModel = new AlunoEstagioViewModel
            {
                Aluno = aluno,
                DataEstagio = DateTime.Now.Date,
                AlunoRa = aluno.RA,
            };

            return View(alunoEstagioViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SolicitarEstagio(AlunoEstagioViewModel estagioViewModel)
        {
            if (ModelState.IsValid)
            {
                var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(estagioViewModel.Instituicao.InstituicaoId);
                var aluno = await _userManager.Users.FirstOrDefaultAsync(a => a.RA == estagioViewModel.AlunoRa);

                if (instituicao == null)
                {
                    ViewBag.ErrorMessage = "Instituição não encontrada.";
                    return View("InstituicaoNaoEncontrada");
                }

                var estagio = new Estagio
                {
                    EstagioId = Guid.NewGuid(),
                    AlunoId = aluno.Id,
                    Aluno = aluno,
                    DataEstagio = estagioViewModel.DataEstagio,
                    Instituicao = instituicao,
                    StatusEstagio = EstagioEnum.Solicitado,
                };

                await _estagioRepository.CriarEstagio(estagio);


                return RedirectToAction("Estagios");
            }

            return View(estagioViewModel);
        }
        public IActionResult Presencas()
        {
            return View();
        }
    }
}
