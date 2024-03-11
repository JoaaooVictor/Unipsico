using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;

namespace AppUnipsico.Repositories
{
    public class ConsultaRepository
    {
        private readonly AppUnipsicoDb _context;

        public ConsultaRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public IEnumerable<Consulta> BuscaTodasConsultasPorStatus(ConsultaEnum statusConsulta)
        {
            return _context.Consultas.Where(c => c.StatusConsulta == statusConsulta).ToList();
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
    }
}
