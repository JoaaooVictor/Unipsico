using System.Drawing;

namespace AppUnipsico.Models
{
    public class Estagio
    {
        public Guid EstagioId { get; set; }
        public DateTime DataInicioEstagio { get; set; }
        public DateTime DataFimEstagio { get; set; }

        public string AlunoId { get; set; }
        public virtual Usuario Aluno { get; set; }

        public Guid InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
    }
}
