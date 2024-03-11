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

        public IEnumerable<DataDisponivel> BuscaTodasDatasDisponiveis()
        {
            return _context.DatasDisponiveis.ToList();
        }

        public async Task SalvarConsultasExcel(List<DataDisponivel> datasDisponiveis)
        {
            await _context.AddRangeAsync(datasDisponiveis);
            await _context.SaveChangesAsync();
        }
    }
}
