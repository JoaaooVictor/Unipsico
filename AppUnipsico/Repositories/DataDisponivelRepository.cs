using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;

namespace AppUnipsico.Repositories
{
    public class DataDisponivelRepository
    {
        private readonly AppUnipsicoDb _context;

        public DataDisponivelRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public IEnumerable<Datas> BuscaTodasDatasDisponiveis()
        {
            return _context.Datas.ToList();
        }

        public async Task SalvarConsultasExcel(List<Datas> datasDisponiveis)
        {
            await _context.AddRangeAsync(datasDisponiveis);
            await _context.SaveChangesAsync();
        }
    }
}
