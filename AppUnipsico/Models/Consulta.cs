using AppUnipsico.Enums;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace AppUnipsico.Models
{
    public class Consulta
    {
        public Guid ConsultaId { get; set; }
        public DateTime DataConsulta { get; set; }
        public ConsultaEnum StatusConsulta { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
