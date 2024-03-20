using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminConsultasController : Controller
    {
        private readonly ConsultasRepository _consultasRepository;
        private readonly DatasRepository _datasRepository;
        private readonly UserManager<Usuario> _userManager;

        public AdminConsultasController(ConsultasRepository consultasRepository, DatasRepository datasRepository, UserManager<Usuario> userManager)
        {
            _consultasRepository = consultasRepository;
            _datasRepository = datasRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var consultas = await _consultasRepository.BuscaTodasConsultas();

            return View(consultas);
        }

        [HttpGet]
        public async Task<IActionResult> CriarConsulta()
        {
            ViewBag.DatasDisponiveis = await _datasRepository.BuscaTodasDatasDisponiveis();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarConsultaConfirmada(CriaConsultaViewModel criaConsultaViewModel)
        {
            if (ModelState.IsValid)
            {
                var paciente = _userManager.Users.FirstOrDefault(c => c.Cpf == criaConsultaViewModel.CpfPaciente 
                                                                && c.TipoUsuario == Enums.TipoUsuarioEnum.Paciente); // Adicionar campo de Ativo

                if(paciente is null)
                {
                    return View(criaConsultaViewModel);
                }


            }

            return View();
        }
    }
}
