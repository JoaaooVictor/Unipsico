using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class EstagioRepository
    {
        private readonly AppUnipsicoDb _context;

        public EstagioRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estagio>> BuscaTodosEstagios()
        {
            return await _context.Estagios
                            .Include(e => e.Aluno)
                            .Include(e => e.Instituicao)
                            .ToListAsync();
        }

        public async Task<Estagio> BuscaEstagioPorId(Guid estagioId)
        {
            return await _context.Estagios
                            .Where(e => e.EstagioId == estagioId)
                            .Include(e => e.Aluno)
                            .Include(e => e.Instituicao)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Estagio>> BuscarEstagiosPorData(DateTime dataInicio)
        {
            return await _context.Estagios
                .Where(e => e.DataEstagio >= dataInicio)
                .Include(e => e.Aluno)
                .Include(e => e.Instituicao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Estagio>> BuscarEstagiosPorRaAluno(string raAluno)
        {
            return await _context.Estagios
                .Where(e => e.Aluno.RA == raAluno)
                .Include(e => e.Aluno)
                .Include(e => e.Instituicao)
                .ToListAsync();
        }


        public async Task CriarEstagio(Estagio estagio)
        {
            await _context.Estagios.AddAsync(estagio);
            await _context.SaveChangesAsync();
        }

        public async Task<string> RenderizarHtmlEstagio(Estagio estagio)
        {
            var htmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), @"Documentos\EstagioTemplate.html");
            var htmlContent = await File.ReadAllTextAsync(htmlFilePath);

            htmlContent = htmlContent.Replace("{{NomeDoAluno}}", estagio.Aluno.NomeCompleto);
            htmlContent = htmlContent.Replace("{{NumeroDoRa}}", estagio.Aluno.RA);
            htmlContent = htmlContent.Replace("{{AlunoRa}}", estagio.Aluno.RA);
            htmlContent = htmlContent.Replace("{{Endereco}}", estagio.Instituicao.Logradouro);
            htmlContent = htmlContent.Replace("{{DataInicioEstagio}}", estagio.DataEstagio.ToShortDateString());
            htmlContent = htmlContent.Replace("{{NomeInstituicaoEstagio}}", estagio.Instituicao.Nome);
            htmlContent = htmlContent.Replace("{{DataDaAssinatura}}", DateTime.Now.ToLongDateString());


            return htmlContent;
        }
    }
}
