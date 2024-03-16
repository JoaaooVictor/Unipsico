using AppUnipsico.Models;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class EstagioViewModel
    {
        public DateTime DataInicioEstagio { get; set; }
        public DateTime DataFinalEstagio { get; set; }
        public string AlunoRa { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}
