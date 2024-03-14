using AppUnipsico.Models;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class UsuarioDetalheViewModel
    {
        public string Mensagem { get; set; }
        public List<Consulta> Consultas { get; set; }
        public Usuario Usuario { get; set; }
    }
}
