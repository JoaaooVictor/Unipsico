using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IDataService
    {
        public IEnumerable<Datas> DatasDisponiveis();
        public Task InserirDatasDisponiveis();
        public Task AtualizaStatusData(Guid dataId);
        public Task<Datas> BuscaDataPorId(Guid dataId);
    }
}
