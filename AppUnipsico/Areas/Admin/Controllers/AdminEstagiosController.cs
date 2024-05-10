using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminProfessorRole")]
    public class AdminEstagiosController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly EstagioRepository _estagioRepository;
        private readonly InstituicoesRepository _instituicoesRepository;

        public AdminEstagiosController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, EstagioRepository estagioRepository, InstituicoesRepository instituicoesRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _estagioRepository = estagioRepository;
            _instituicoesRepository = instituicoesRepository;
        }

        public async Task<IActionResult> Index(DateTime? dataInicio, string raAluno)
        {
            List<Estagio> estagios;

            if (!string.IsNullOrEmpty(raAluno))
            {
                estagios = await _estagioRepository.BuscarEstagiosPorRaAluno(raAluno);
            }
            else if (dataInicio != null)
            {
                estagios = await _estagioRepository.BuscarEstagiosPorData((DateTime)dataInicio);
            }
            else
            {
                estagios = await _estagioRepository.BuscaTodosEstagios();
            }

            if (estagios == null)
            {
                return NotFound();
            }

            return View(estagios);
        }

        [HttpGet]
        public async Task<IActionResult> CriarEstagio()
        {
            ViewBag.Instituicoes = await _instituicoesRepository.BuscarInstituicoes();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarEstagio(EstagioViewModel estagioViewModel)
        {
            if (ModelState.IsValid)
            {
                var aluno = await _userManager.Users.FirstOrDefaultAsync(u => u.RA == estagioViewModel.AlunoRa);
                var instituicao = await _instituicoesRepository.BuscarIntituicaoPorId(estagioViewModel.Instituicao.InstituicaoId);

                if (aluno is null)
                {
                    ViewBag.ErrorMessage = $"Aluno com RA: {estagioViewModel.AlunoRa} não encontrado.";
                    return View("AlunoNaoEncontrado");
                }

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
                    StatusEstagio = EstagioEnum.Aprovado,
                };

                await _estagioRepository.CriarEstagio(estagio);


                return RedirectToAction("Index");
            }

            ViewBag.Instituicoes = await _instituicoesRepository.BuscarInstituicoes();
            return View(estagioViewModel);
        }

        public async Task<IActionResult> Detalhes(Guid estagioId)
        {
            if (string.IsNullOrEmpty(estagioId.ToString()))
            {
                return NotFound();
            }

            var estagio = await _estagioRepository.BuscaEstagioPorId(estagioId);

            return View(estagio);
        }

        public async Task<IActionResult> GerarPdfEstagio(Guid estagioId)
        {
            var estagio = await _estagioRepository.BuscaEstagioPorId(estagioId);

            if (estagio == null)
            {
                return NotFound();
            }

            var conteudoHtml = await _estagioRepository.RenderizarHtmlEstagio(estagio);
            var nomePdf = $"{estagio.AlunoId}-{estagio.EstagioId}.pdf";

            try
            {
                var memoryStream = new MemoryStream();
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var converter = new ConverterProperties();

                HtmlConverter.ConvertToPdf(conteudoHtml, pdf, converter);

                pdf.Close();

                return File(memoryStream.ToArray(), "application/pdf", nomePdf);

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> AprovarEstagio(Guid estagioId)
        {
            var estagio = await _estagioRepository.BuscaEstagioPorId(estagioId);

            if (estagio is not null)
            {
                estagio.StatusEstagio = EstagioEnum.Aprovado;
                await _estagioRepository.AtualizaStatusEstagio(estagio);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReprovarEstagio(Guid estagioId)
        {
            var estagio = await _estagioRepository.BuscaEstagioPorId(estagioId);

            if (estagio is not null)
            {
                estagio.StatusEstagio = EstagioEnum.Reprovado;
                await _estagioRepository.AtualizaStatusEstagio(estagio);
            }

            return RedirectToAction("Index");
        }
    }
}
