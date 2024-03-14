using AppUnipsico.Models;
using AppUnipsico.Services.Interfaces;
using AppUnipsico.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace AppUnipsico.Controllers
{
    [Authorize(Policy = "RequirePacienteRole")]
    public class ConsultaController : Controller
    {
        private readonly IConsultaService _consultaService;
        private readonly UserManager<Usuario> _userManager;
        private readonly IDataService _dataService;

        public ConsultaController(IConsultaService consultaService, UserManager<Usuario> userManager, IDataService dataService)
        {
            _consultaService = consultaService;
            _userManager = userManager;
            _dataService = dataService;
        }

        public async Task<IActionResult> Agendar(Guid DataConsultaId)
        {
            var usuario = await _userManager.GetUserAsync(User);

            if (usuario is null)
            {
                return RedirectToAction("Login", "Account");
            }

            var consulta = await _consultaService.CriaConsulta(DataConsultaId, usuario);
            if (consulta is not null)
            {
                await _dataService.AtualizaStatusData(consulta.DataConsultaId);
                return View("ConsultaAgendada", consulta);
            }

            return View();
        }

        public IActionResult ConsultaAgendada(Guid IdConsulta)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConsultaPaciente()
        {
            var usuario = await _userManager.GetUserAsync(User);

            var consultas = await _consultaService.BuscaConsultaPorUsuario(usuario);

            if (consultas is not null)
            {
                var consultaViewModel = new ConsultaViewModel
                {
                    Consultas = consultas,
                };
                
                return View(consultaViewModel);
            }
            else
            {
                ViewBag.MensagemInfo = "Nenhuma consulta encontrada!";
                return View();
            }
        }

        public async Task<IActionResult> ValidaEConfirmaConsultaNaSemana(Guid dataConsultaId)
        {
            var usuario = await _userManager.GetUserAsync(User);

            Consulta consultaNaSemana = await _consultaService.VerificaConsultaNaSemana(usuario, dataConsultaId);

            if (consultaNaSemana is not null)
            {
                var retornoModal = new ValidaConsultaViewModel
                {
                    Usuario = usuario,
                    Datas = consultaNaSemana.DataConsulta,
                    DataConsultaId = consultaNaSemana.DataConsultaId,
                    Consulta = consultaNaSemana,
                    ConsultaId = consultaNaSemana.DataConsultaId,
                    Mensagem = "Você já possui uma consulta agendada para esta semana.",
                };
                return View(retornoModal);
            }
            else
            {
                var data = await _dataService.BuscaDataPorId(dataConsultaId);

                var retornoModal = new ValidaConsultaViewModel
                {
                    Usuario = usuario,
                    DataConsultaId = data.Id,
                    Datas = data,
                    Consulta = null,
                    ConsultaId = null,
                    Mensagem = "Confirmar Consulta",
                };

                return View("ModalConfirmacaoConsulta", retornoModal);
            }
        }

    }
}
