using AppUnipsico.Areas.Admin.Repositories;
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
    public class AdminInstituicoesController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly InstituicoesRepository _instituicoesRepository;

        public AdminInstituicoesController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, InstituicoesRepository instituicoesRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _instituicoesRepository = instituicoesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var instituicoes = await _instituicoesRepository.BuscarInstituicoes();

            return View(instituicoes);
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
                    Nome = instituicaoViewModel.Nome,
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
    }
}
