using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                var formatadoCpf = FormatUtility.RemovePontoTracoCpf(criaConsultaViewModel.CpfPaciente);
                var paciente = await _userManager.Users.FirstOrDefaultAsync(c => c.Cpf == formatadoCpf
                                                                && c.TipoUsuario == TipoUsuarioEnum.Paciente);

                if (paciente is null)
                {
                    return View("PacienteNaoCadastrado");
                }

                var dataConsulta = await _datasRepository.BuscaDataPorId(criaConsultaViewModel.DataId);

                if (dataConsulta is null)
                {
                    return View("DataNaoEncotrada");
                }

                var consulta = new Consulta
                {
                    ConsultaId = Guid.NewGuid().ToString(),
                    StatusConsulta = ConsultaEnum.Agendada,
                    DataConsultaId = dataConsulta.Id,
                    DataConsulta = dataConsulta,
                    Usuario = paciente,
                    UsuarioId = paciente.Id,
                };

                var sucesso = await _consultasRepository.CriaConsulta(consulta);

                if (!sucesso.Erro)
                {
                    await _datasRepository.AtualizaStatusDaData(dataConsulta.Id, ConsultaEnum.Agendada);
                    return RedirectToAction("Index");
                }

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DetalhesConsulta(string consultaId)
        {
            var consulta = await _consultasRepository.BuscaConsultaPorId(consultaId);

            if (consulta is null)
            {
                return NotFound();
            }

            return View(consulta);
        }
    }
}
