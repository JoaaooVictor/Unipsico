using Microsoft.AspNetCore.Identity;

namespace AppUnipsico.Models
{
    public class User : IdentityUser
    {
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
