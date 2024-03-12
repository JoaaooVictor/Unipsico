using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IDataDisponivelService
    {
        public Task<IEnumerable<Datas>> DatasDisponiveis();
        public Task InserirDatasDisponiveis();
    }
}
