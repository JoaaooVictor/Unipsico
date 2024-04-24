using AppUnipsico.Models;
using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class EstagioViewModel
    {
        [Required(ErrorMessage = "O campo data é obrigatório.")]
        public DateTime DataEstagio { get; set; }

        [Required(ErrorMessage = "O campo RA é obrigatório.")]
        public string AlunoRa { get; set; }

        [Required(ErrorMessage = "O de instituição é obrigatório.")]
        public Instituicao Instituicao { get; set; }
    }
}
