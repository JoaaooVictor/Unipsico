namespace AppUnipsico.Models
{
    public class Instituicao
    {
        public Guid InstituicaoId { get; set; }
        public string NomeInstituicao { get; set; }
        public string NomeResponsavelInstituicao { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }

        public virtual IEnumerable<Estagio> Estagios { get; set; }
    }
}
