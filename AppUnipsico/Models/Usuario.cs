using AppUnipsico.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.Models
{
    public class Usuario : IdentityUser
    {
        [Display(Name = "Nome completo")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Cpf")]
        public string Cpf { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data do cadastro")]
        public DateTime DataRegistro { get; set; }

        [Display(Name = "Tipo de Usuário")]
        public TipoUsuarioEnum TipoUsuario { get; set; }

        public virtual ICollection<Consulta> Consultas { get; set; }
        public virtual ICollection<Estagio> Estagios { get; set; }
    }
}
