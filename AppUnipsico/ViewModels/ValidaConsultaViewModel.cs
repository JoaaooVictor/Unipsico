using AppUnipsico.Models;
using System.Drawing;

namespace AppUnipsico.ViewModels
{
    public class ValidaConsultaViewModel
    {
        public string Mensagem { get; set; }

        public Guid? DataConsultaId { get; set; }
        public Datas? Datas { get; set; }

        public Usuario Usuario { get; set; }

        public Consulta? Consulta { get; set; }
        public Guid? ConsultaId { get; set; }
    }
}
