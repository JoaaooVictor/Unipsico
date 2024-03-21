using AppUnipsico.Data.Context;
using AppUnipsico.Models;
using AppUnipsico.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AppUnipsico.Areas.Admin.Repositories
{
    public class ConsultasRepository
    {
        private readonly AppUnipsicoDb _context;

        public ConsultasRepository(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Consulta>> BuscaTodasConsultas()
        {
            return await _context.Consultas
                .Include(c => c.Usuario)
                .Include(c => c.DataConsulta)
                .ToListAsync();
        }

        public async Task<TrataRetornoDto> CriaConsulta(Consulta consulta)
        {
            var trataRetornoDto = new TrataRetornoDto();

            try
            {
                await _context.Consultas.AddAsync(consulta);
                await _context.SaveChangesAsync();

                trataRetornoDto.Erro = false;
                trataRetornoDto.Mensagem = "Consulta Registrada com sucesso!";
            }
            catch (Exception ex)
            {
                trataRetornoDto.Erro = true;
                trataRetornoDto.Mensagem = $"Erro ao registrar a consulta! Erro:{ex.Message}";
            }

            return trataRetornoDto;
        }

        public async Task<Consulta> BuscaConsultaPorId(string consultaId)
        {
            return await _context.Consultas
                                    .Where(c => c.ConsultaId == consultaId)
                                    .Include(c => c.Usuario)
                                    .Include(c => c.DataConsulta)
                                    .FirstOrDefaultAsync();
        }
    }
}
