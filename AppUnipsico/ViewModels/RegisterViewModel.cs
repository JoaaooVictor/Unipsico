using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Preencha o nome!")]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Preencha a senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Preencha a senha novamente!")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Repassword { get; set; }

        [Required(ErrorMessage = "Preencha o cpf!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Preencha o email!")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha a data de nascimento!")]
        [Display(Name = "Data do Nascimento")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }

        public string ReturnUrl { get; set; }
    }
}
