using AppUnipsico.Enums;

namespace AppUnipsico.Models
{
    public class Consulta
    {
        public string ConsultaId { get; set; }
        public ConsultaEnum StatusConsulta { get; set; }

        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public Guid DataConsultaId { get; set; }
        public virtual Datas DataConsulta { get; set; }
    }
}
