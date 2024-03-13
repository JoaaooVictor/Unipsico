using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.Repositories;
using AppUnipsico.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Services.Impl
{
    public class ConsultaService : IConsultaService
    {
        private readonly ConsultaRepository _consultaRepository;

        public ConsultaService(ConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        public async Task<Consulta> CriaConsulta(Guid dataConsultaId, Usuario usuario)
        {

            var consulta = new Consulta
            {
                ConsultaId = Guid.NewGuid().ToString(),
                UsuarioId = usuario.Id,
                Usuario = usuario,
                StatusConsulta = ConsultaEnum.Agendada,
                DataConsultaId = dataConsultaId,
            };

            await _consultaRepository.CriaConsulta(consulta);

            return consulta;
        }

        public async Task<Consulta> BuscaConsultaPorId(Guid consultaId)
        {
            return await _consultaRepository.BuscaConsultaPorId(consultaId);
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaDisponiveis()
        {
            return await _consultaRepository.BuscaConsultaPorStatus(ConsultaEnum.Disponivel);
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaRemarcadas()
        {
            return await _consultaRepository.BuscaConsultaPorStatus(ConsultaEnum.Remarcada);
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaCanceladas()
        {
            return await _consultaRepository.BuscaConsultaPorStatus(ConsultaEnum.Cancelada);
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaConcluidas()
        {
            return await _consultaRepository.BuscaConsultaPorStatus(ConsultaEnum.Concluida);
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaPorUsuario(Usuario usuario)
        {
            return await _consultaRepository.BuscaConsultasPorUsuario(usuario);
        }

        public async Task<Consulta> VerificaConsultaNaSemana(Usuario usuario, Guid dataNovaConsultaId)
        {
            return await _consultaRepository.ConsultaExisteNaMesmaSemana(usuario, dataNovaConsultaId);
        }
    }
}
