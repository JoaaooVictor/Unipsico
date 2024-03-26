using AppUnipsico.Models;
using AppUnipsico.Repositories;
using AppUnipsico.Services.Interfaces;
using OfficeOpenXml;

namespace AppUnipsico.Services.Impl
{
    public class DataService : IDataService
    {
        private readonly DataRepository _dataRepository;

        public DataService(DataRepository dataDisponivelRepository)
        {
            _dataRepository = dataDisponivelRepository;
        }

        public IEnumerable<Datas> DatasDisponiveis()
        {
            return _dataRepository.BuscaTodasDatasDisponiveis();
        }

        public async Task AtualizaStatusData(Guid dataId)
        {
            await _dataRepository.AtualizaStatusData(dataId);
        }

        public async Task InserirDatasDisponiveis(IFormFile file)
        {
            if (file is not null)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet is not null)
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        var datas = new List<Datas>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var dataString = worksheet.Cells[row, 1].Value.ToString();
                            if (DateTime.TryParse(dataString, out DateTime dataHora))
                            {
                                datas.Add(new Datas { Data = dataHora, Id = Guid.NewGuid(), Status = Enums.ConsultaEnum.Disponivel });
                            }
                        }
                        await _dataRepository.SalvarConsultasExcel(datas);
                    }
                }
            }
        }

        public async Task<Datas> BuscaDataPorId(Guid dataId)
        {
            return await _dataRepository.BuscaDataConsultaPorId(dataId);
        }
    }
}
