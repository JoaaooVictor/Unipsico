using AppUnipsico.Areas.Admin.Repositories;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Controllers
{
    [AllowAnonymous]
    public class EstagiosController : Controller
    {
        private readonly EstagioRepository _estagioRepository;

        public EstagiosController(EstagioRepository repository)
        {
            _estagioRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscaEstagio(Guid estagioId)
        {
           var estagio = await _estagioRepository.BuscaEstagioPorId(estagioId);

            if(estagio is null)
            {
                return View("EstagioNaoEncontrado");
            }

            return View("Detalhes", estagio);
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
