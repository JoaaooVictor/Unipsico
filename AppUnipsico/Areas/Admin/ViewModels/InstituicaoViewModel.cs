using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class InstituicaoViewModel
    {
        [Required(ErrorMessage = "O nome da instituição é obrigatório.")]
        [Display(Name = "Nome da Instituição")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O nome do responsável é obrigatório.")]
        [Display(Name = "Responsável pela Instituição")]
        public string NomeResponsavelInstituicao { get; set; }

        [Required(ErrorMessage = "O logradouro da instituição é obrigatório.")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Complemento da instituição é obrigatório.")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O número da instituição é obrigatório.")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O Bairro da instituição é obrigatório.")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade da instituição é obrigatório.")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O Cep da instituição é obrigatório.")]
        [Display(Name = "Cep")]
        public string Cep { get; set; }
    }
}
