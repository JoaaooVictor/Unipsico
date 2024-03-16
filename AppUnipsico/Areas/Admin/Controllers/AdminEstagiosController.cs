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
    [Authorize(Policy = "RequireAdminRole")]
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

        public async Task<IActionResult> Index()
        {
            var estagios = await _estagioRepository.BuscaTodosEstagios();

            if (estagios is null)
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

                if (aluno != null && instituicao != null)
                {
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
                }

                return RedirectToAction("Index");
            }

            return View(estagioViewModel);
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

            return File(memoryStream.ToArray(), "application/pdf", Path.Combine($"{estagio.Aluno.RA}","_estagio.pdf"));
        }
    }
}
