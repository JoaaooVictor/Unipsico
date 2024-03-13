using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUnipsico.Areas.Aluno.Controllers
{
    [Area("Aluno")]
    [Authorize(Policy = "RequireProfessorAlunoRole")]
    public class AlunoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
