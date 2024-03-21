using AppUnipsico.Models;
using System.ComponentModel.DataAnnotations;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class CriaConsultaViewModel
    {
        public Guid DataId { get; set; }

        [StringLength(14, MinimumLength = 14, ErrorMessage = "O CPF deve conter 11 dígitos")]
        public string CpfPaciente { get; set; }
    }
}
