using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class ProfessorRepository
    {
        private readonly AppUnipsicoDb _context;

        public ProfessorRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> BuscaTodosProfessores()
        {
            return await _context.Users.Where(u => u.TipoUsuario == Enums.TipoUsuarioEnum.Professor).ToListAsync();
        }
    }
}
