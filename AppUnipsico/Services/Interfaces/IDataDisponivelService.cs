using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IDataDisponivelService
    {
        public IEnumerable<Datas> DatasDisponiveis();
        public Task InserirDatasDisponiveis();
    }
}
