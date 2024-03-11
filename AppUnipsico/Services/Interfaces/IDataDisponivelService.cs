using AppUnipsico.Models;

namespace AppUnipsico.Services.Interfaces
{
    public interface IDataDisponivelService
    {
        public Task<IEnumerable<DataDisponivel>> DatasDisponiveis();
        public Task InserirDatasDisponiveis();
    }
}
