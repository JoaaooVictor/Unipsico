using AppUnipsico.Enums;

namespace AppUnipsico.Models
{
    public class Consulta
    {
        public string ConsultaId { get; set; }
        public DateTime DataConsulta { get; set; }
        public ConsultaEnum StatusConsulta { get; set; }

        public Guid UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
