using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IDataDisponivelService
    {
        public IEnumerable<DataDisponivel> DatasDisponiveis();
        public Task InserirDatasDisponiveis();
    }
}
