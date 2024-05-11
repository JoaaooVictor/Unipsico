using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using Humanizer;
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

        public async Task<List<Estagio>> BuscaTodosEstagios()
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

        public async Task<List<Estagio>> BuscarEstagiosPorData(DateTime dataInicio)
        {
            return await _context.Estagios
                .Where(e => e.DataEstagio >= dataInicio)
                .Include(e => e.Aluno)
                .Include(e => e.Instituicao)
                .ToListAsync();
        }

        public async Task<List<Estagio>> BuscarEstagiosPorRaAluno(string raAluno)
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
            var caminhoArquivoHtml = Path.Combine(Directory.GetCurrentDirectory(), @"Documentos\EstagioTemplate2.html");
            var conteudoHtml = await File.ReadAllTextAsync(caminhoArquivoHtml);

            conteudoHtml = conteudoHtml.Replace("{{NomeDoAluno}}", estagio.Aluno.NomeCompleto);
            conteudoHtml = conteudoHtml.Replace("{{NomeResponsavel}}", estagio.Instituicao.NomeResponsavelInstituicao);
            conteudoHtml = conteudoHtml.Replace("{{NumeroDoRa}}", estagio.Aluno.RA);
            conteudoHtml = conteudoHtml.Replace("{{AlunoRa}}", estagio.Aluno.RA);
            conteudoHtml = conteudoHtml.Replace("{{Endereco}}", estagio.Instituicao.Logradouro);
            conteudoHtml = conteudoHtml.Replace("{{DataInicioEstagio}}", estagio.DataEstagio.ToShortDateString());
            conteudoHtml = conteudoHtml.Replace("{{NomeInstituicaoEstagio}}", estagio.Instituicao.NomeInstituicao);
            conteudoHtml = conteudoHtml.Replace("{{DataDaAssinatura}}", DateTime.Now.ToLongDateString());
            conteudoHtml = conteudoHtml.Replace("{{EstagioId}}", estagio.EstagioId.ToString());
            conteudoHtml = conteudoHtml.Replace("{{Dia}}", DateTime.Now.Day.ToString());
            conteudoHtml = conteudoHtml.Replace("{{Mes}}", DateTime.Now.ToString("MMMM"));
            conteudoHtml = conteudoHtml.Replace("{{Ano}}", DateTime.Now.Year.ToString());

            return conteudoHtml;
        }

        public async Task AtualizaStatusEstagio(Estagio estagio)
        {
            _context.Estagios.Update(estagio);
            await _context.SaveChangesAsync();
        }
    }
}
