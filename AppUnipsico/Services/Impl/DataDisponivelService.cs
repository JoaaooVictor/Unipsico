using AppUnipsico.Data.Context;
using AppUnipsico.Enums;
using AppUnipsico.Models;
using AppUnipsico.Repositories;
using AppUnipsico.Services.Interfaces;
using OfficeOpenXml;

namespace AppUnipsico.Services.Impl
{
    public class DataDisponivelService : IDataDisponivelService
    {
        private readonly DataDisponivelRepository _dataDisponivelRepository;

        public DataDisponivelService(DataDisponivelRepository dataDisponivelRepository)
        {
            _dataDisponivelRepository = dataDisponivelRepository;
        }

        public IEnumerable<Datas> DatasDisponiveis()
        {
            return _dataDisponivelRepository.BuscaTodasDatasDisponiveis();
        }

        public async Task InserirDatasDisponiveis()
        {
            var caminhoArquivo = @"C:\Users\joaov\OneDrive\Documentos\Documentos PCC\dataconsulta.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(caminhoArquivo)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet is not null)
                {
                    int rowCount = worksheet.Dimension.Rows;
                    var consultas = new List<Datas>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var dataString = worksheet.Cells[row, 1].Value.ToString();
                        if (DateTime.TryParse(dataString, out DateTime dataHora))
                        {
                            consultas.Add(new Datas { Data = dataHora, Id = Guid.NewGuid() });
                        }
                    }

                    await _dataDisponivelRepository.SalvarConsultasExcel(consultas);
                }
            }
        }
    }
}
