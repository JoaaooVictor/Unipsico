using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.ViewModels
{
    public class RegistraUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage = "A senha deve conter pelo menos 8 caracteres, uma letra maiúscula, um número e um caractere especial.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Confirme a Senha é obrigatório.")]
        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string Repassword { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório")]
        [Display(Name = "Data do Nascimento")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Selecione um perfil!")]
        [Display(Name = "Perfil")]
        public string SelectedRole { get; set; }

        public string ReturnUrl { get; set; }

        public string RA { get; set; }
    }
}
