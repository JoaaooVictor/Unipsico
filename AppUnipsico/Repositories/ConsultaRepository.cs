using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Repositories
{
    public class ConsultaRepository
    {
        private readonly AppUnipsicoDb _context;

        public ConsultaRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task SalvarConsultasExcel(List<Consulta> consultas)
        {
            try
            {
                await _context.AddRangeAsync(consultas);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<Consulta> BuscaConsultaPorId(Guid consultaId)
        {
            return await _context.Consultas.Where(c => c.ConsultaId == consultaId.ToString()).FirstAsync();
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultaPorStatus(ConsultaEnum status)
        {
            return await _context.Consultas.Where(c => c.StatusConsulta == status).ToListAsync();
        }

        public async Task CriaConsulta(Consulta consulta)
        {
            await _context.Consultas.AddAsync(consulta);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Consulta>> ListaTodasConsultasCriadas()
        {
            return await _context.Consultas.ToListAsync();
        }

        public async Task<IEnumerable<Consulta>> BuscaConsultasPorUsuario(Usuario usuario)
        {
            return await _context.Consultas
                        .Where(c => c.UsuarioId == usuario.Id)
                        .Include(c => c.Usuario)
                        .Include(c => c.DataConsulta)
                        .OrderBy(c => c.DataConsulta.Data)
                        .ToListAsync();
        }

        public async Task<Consulta> ConsultaExisteNaMesmaSemana(Usuario usuario, Guid dataNovaConsultaId)
        {
            var dataNovaConsulta = await _context.Datas.FindAsync(dataNovaConsultaId);

            if (dataNovaConsulta == null)
            {
                return null;
            }

            DateTime inicioSemanaNovaConsulta = dataNovaConsulta.Data.AddDays(-(int)dataNovaConsulta.Data.DayOfWeek);
            DateTime fimSemanaNovaConsulta = inicioSemanaNovaConsulta.AddDays(6).Date;

            IEnumerable<Consulta> consulta = await _context.Consultas.Where(c => c.UsuarioId == usuario.Id)
                                                                      .Include(c => c.DataConsulta)
                                                                      .ToListAsync();

            var consultaNaSemana = consulta.Where(c => c.DataConsulta.Data.Date >= inicioSemanaNovaConsulta.Date &&
                                                                      c.DataConsulta.Data.Date <= fimSemanaNovaConsulta.Date).FirstOrDefault();

            return consultaNaSemana;

        }


    }
}
