using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminInstituicoesController : Controller
    {

        private readonly InstituicoesRepository _instituicoesRepository;

        public AdminInstituicoesController(InstituicoesRepository instituicoesRepository)
        {
            _instituicoesRepository = instituicoesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var instituicoes = await _instituicoesRepository.BuscarInstituicoes();

            return View(instituicoes);
        }

        public async Task<IActionResult> DetalhesInstituicao(Guid instituicaoId)
        {
            if (string.IsNullOrEmpty(instituicaoId.ToString()))
            {
                return NotFound();
            }

            var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(instituicaoId);

            if (instituicao == null)
            {
                return NotFound();
            }

            return View(instituicao);
        }

        [HttpGet]
        public IActionResult CriarInstituicao()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarInstituicao(InstituicaoViewModel instituicaoViewModel)
        {
            if (ModelState.IsValid)
            {
                var instituicao = new Instituicao()
                {
                    InstituicaoId = Guid.NewGuid(),
                    NomeInstituicao = instituicaoViewModel.Nome,
                    NomeResponsavelInstituicao = instituicaoViewModel.NomeResponsavelInstituicao,
                    Logradouro = instituicaoViewModel.Logradouro,
                    Complemento = instituicaoViewModel.Complemento,
                    Cidade = instituicaoViewModel.Cidade,
                    Bairro = instituicaoViewModel.Bairro,
                    Cep = instituicaoViewModel.Cep,
                    Numero = instituicaoViewModel.Numero,
                };

                await _instituicoesRepository.CriarInstituicao(instituicao);

                return RedirectToAction("Index", "AdminInstituicoes");
            }
            else
            {
                return View(instituicaoViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarInstituicao(Guid instituicaoId)
        {
            if (string.IsNullOrEmpty(instituicaoId.ToString()))
            {
                return NotFound();
            }

            var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(instituicaoId);

            if (instituicao == null)
            {
                return NotFound();
            }

            return View(instituicao);
        }


        [HttpPost]
        public async Task<IActionResult> SalvarEdicaoInstituicao(Instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                await _instituicoesRepository.SalvarEdicaoInstituicao(instituicao);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditarInstituicao", instituicao);
            }
        }

        public async Task<IActionResult> DeletarInstituicao(Guid instituicaoId)
        {
            if (string.IsNullOrEmpty(instituicaoId.ToString()))
            {
                return NotFound();
            }

            var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(instituicaoId);

            if (instituicao == null)
            {
                return NotFound();
            }

            return View(instituicao);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarInstituicaoConfirmada(Guid instituicaoId)
        {
            var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(instituicaoId);
            if (instituicao != null)
            {
                await _instituicoesRepository.DeletarInstituicao(instituicao);
            }

            return RedirectToAction("Index");
        }
    }
}
