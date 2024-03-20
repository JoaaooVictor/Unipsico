using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class ConsultasRepository
    {
        private readonly AppUnipsicoDb _context;

        public ConsultasRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> BuscaTodasConsultas()
        {
            return  await _context.Consultas
                .Include(c => c.Usuario)
                .Include(c => c.DataConsulta)
                .ToListAsync();
        }
    }
}
