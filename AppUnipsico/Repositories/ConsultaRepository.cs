using AppUnipsico.Data.Context;

namespace AppUnipsico.Repositories
{
    public class ConsultaRepository
    {
        private readonly AppUnipsicoDb _context;

        public ConsultaRepository(AppUnipsicoDb context)
        {
            _context = context;
        }
    }
}
