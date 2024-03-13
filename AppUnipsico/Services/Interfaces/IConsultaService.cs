using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IConsultaService
    {
        public Task<Consulta> BuscaConsultaPorId(Guid consultaId);
        public Task<IEnumerable<Consulta>> BuscaConsultaDisponiveis();
        public Task<IEnumerable<Consulta>> BuscaConsultaRemarcadas();
        public Task<IEnumerable<Consulta>> BuscaConsultaCanceladas();
        public Task<IEnumerable<Consulta>> BuscaConsultaConcluidas();
        public Task<IEnumerable<Consulta>> BuscaConsultaPorUsuario(Usuario usuario);
        public Task<Consulta> CriaConsulta(Guid DataConsultaId, Usuario usuario);
        public Task<Consulta> VerificaConsultaNaSemana(Usuario usuario, Guid dataNovaConsultaId);
    }
}
