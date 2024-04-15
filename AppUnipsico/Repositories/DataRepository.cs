using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Repositories
{
    public class DataRepository
    {
        private readonly AppUnipsicoDb _context;

        public DataRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public IEnumerable<Datas> BuscaTodasDatasDisponiveis()
        {
            return _context.Datas.Where(d => d.Status == ConsultaEnum.Disponivel).OrderBy(d => d.Data).ToList();
        }

        public IEnumerable<Datas> BuscaDatasPorStatus(ConsultaEnum status)
        {
            return _context.Datas.Where(d => d.Status == status).ToList();
        }

        public async Task SalvarConsultasExcel(List<Datas> datasDisponiveis)
        {
            await _context.AddRangeAsync(datasDisponiveis);
            await _context.SaveChangesAsync();
        }

        public async Task<Datas> BuscaDataConsultaPorId(Guid dataConsultaId)
        {
            return await _context.Datas.Where(c => c.Id == dataConsultaId).FirstAsync();
        }

        public async Task AtualizaStatusData(Guid dataId)
        {
            var dataConsulta = await BuscaDataConsultaPorId(dataId);

            if (dataConsulta is not null)
            {
                dataConsulta.Status = ConsultaEnum.Agendada;
                _context.Datas.Update(dataConsulta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InsereDataUnica(Datas datas)
        {
            await _context.Datas.AddAsync(datas);
            await _context.SaveChangesAsync();
        }
    }
}
