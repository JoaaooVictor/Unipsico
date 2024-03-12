using AppUnipsico.Enums;

namespace AppUnipsico.Models
{
    public class Datas
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public ConsultaEnum Status { get; set; }
    }
}
