using AppUnipsico.Enums;
using Microsoft.AspNetCore.Identity;

namespace AppUnipsico.Models
{
    public class Usuario : IdentityUser
    {
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataRegistro { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
