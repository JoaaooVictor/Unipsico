using AppUnipsico.Areas.Admin.ViewModels;
using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class InstituicoesRepository
    {
        private readonly AppUnipsicoDb _context;

        public InstituicoesRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task CriarInstituicao(Instituicao instituicao)
        {
            await _context.Instituicoes.AddAsync(instituicao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instituicao>> BuscarInstituicoes()
        {
            return await _context.Instituicoes
                                     .Include(i => i.Estagios)
                                     .ToListAsync();
        }
    }
}
