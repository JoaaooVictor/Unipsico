using AppUnipsico.Repositories;
using AppUnipsico.Services.Interfaces;

namespace AppUnipsico.Services.Impl
{
    public class ConsultaService : IConsultaService
    {
        private readonly ConsultaRepository _consultaRepository;

        public ConsultaService(ConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

    }
}
