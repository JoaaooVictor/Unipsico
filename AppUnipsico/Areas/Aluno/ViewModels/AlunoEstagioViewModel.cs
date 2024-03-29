using AppUnipsico.Models;

namespace AppUnipsico.Areas.Aluno.ViewModels
{
    public class AlunoEstagioViewModel
    {
        public DateTime DataEstagio { get; set; }
        public string AlunoRa { get; set; }
        public Usuario Aluno {  get; set; } 
        public Instituicao Instituicao { get; set; }
    }
}
