using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class DatasRepository
    {
        private readonly AppUnipsicoDb _context;

        public DatasRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<List<Datas>> BuscaDatasSemTracking()
        {
            return await _context.Datas.AsNoTracking().OrderBy(c => c.Data).ToListAsync();
        }

        public async Task<Datas> BuscaDataPorId(Guid dataId)
        {
            return await _context.Datas
                            .Where(d => d.Id == dataId)
                            .FirstOrDefaultAsync();
        }

        public async Task DeletarData(Datas data)
        {
             _context.Datas.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
