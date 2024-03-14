using AppUnipsico.Models;

namespace AppUnipsico.Areas.Admin.ViewModels
{
    public class DatasViewModel
    {
        public string Mensagem { get; set; }
        public IEnumerable<Datas> Datas { get; set; }
    }
}
