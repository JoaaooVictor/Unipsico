using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
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
            return await _context.Datas.AsNoTracking()
                                       .OrderByDescending(c => c.Data)
                                       .ThenBy(c => c.Status)
                                       .ToListAsync();
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

        public async Task<List<Datas>> BuscaTodasDatasDisponiveis()
        {
            return await _context.Datas.Where(d => d.Status == ConsultaEnum.Disponivel).OrderBy(d => d.Data).ToListAsync();
        }

        public async Task AtualizaStatusDaData(Guid dataConsultaId, ConsultaEnum consultaEnum)
        {
            var data = await BuscaDataPorId(dataConsultaId);

            if (data is not null)
            {
                data.Status = consultaEnum;
            }

            _context.Datas.Update(data);
            await _context.SaveChangesAsync();
        }
    }
}
