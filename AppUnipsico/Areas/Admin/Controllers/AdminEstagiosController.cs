using AppUnipsico.Areas.Admin.Repositories;
using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
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

        public async Task<IActionResult> Index(DateTime? dataInicio, DateTime? dataFim, string raAluno)
        {
            IEnumerable<Estagio> estagios;

            if (!string.IsNullOrEmpty(raAluno))
            {
                estagios = await _estagioRepository.BuscarEstagiosPorRaAluno(raAluno);
            }
            else if (dataInicio != null && dataFim != null)
            {
                estagios = await _estagioRepository.BuscarEstagiosPorData((DateTime)dataInicio, (DateTime)dataFim);
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
                    DataInicioEstagio = estagioViewModel.DataInicioEstagio,
                    DataFimEstagio = estagioViewModel.DataFinalEstagio,
                    Instituicao = instituicao,
                    InstituicaoId = instituicao.InstituicaoId
                };

                await _estagioRepository.CriarEstagio(estagio);


                return RedirectToAction("Index");
            }

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

            var htmlContent = await _estagioRepository.RenderizarHtmlEstagio(estagio);

            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdf = new PdfDocument(writer);
            var converter = new ConverterProperties();
            HtmlConverter.ConvertToPdf(htmlContent, pdf, converter);
            pdf.Close();

            return File(memoryStream.ToArray(), "application/pdf", Path.Combine($"{estagio.Aluno.RA}", "_estagio.pdf"));
        }
    }
}
