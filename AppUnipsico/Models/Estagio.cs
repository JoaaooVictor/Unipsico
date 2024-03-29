using AppUnipsico.Enums;
using System.Drawing;

namespace AppUnipsico.Models
{
    public class Estagio
    {
        public Guid EstagioId { get; set; }
        public DateTime DataEstagio { get; set; }
        public EstagioEnum StatusEstagio { get; set; }

        public string AlunoId { get; set; }
        public virtual Usuario Aluno { get; set; }

        public Guid InstituicaoId { get; set; }
        public virtual Instituicao Instituicao { get; set; }
    }
}
