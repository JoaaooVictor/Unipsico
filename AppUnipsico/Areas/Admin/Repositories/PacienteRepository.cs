using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class PacienteRepository
    {
        private readonly AppUnipsicoDb _context;

        public PacienteRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<ICollection<Usuario>> BuscaTodosPacientes()
        {
            return await _context.Users.Where(u => u.TipoUsuario == Enums.TipoUsuarioEnum.Paciente).ToListAsync();
        }
    }
}
