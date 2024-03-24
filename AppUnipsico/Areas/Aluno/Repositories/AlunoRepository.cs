using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Aluno.Repositories
{
    public class AlunoRepository
    {
        private readonly AppUnipsicoDb _context;

        public AlunoRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<List<Estagio>> BuscaTodosEstagiosPorAluno(string alunoId)
        {
            return await _context.Estagios
                            .Where(e => e.AlunoId == alunoId)
                            .Include(e => e.Aluno)
                            .Include(e => e.Instituicao)
                            .ToListAsync();
        }
    }
}
