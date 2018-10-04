using Lottery.Data;
using Lottery.Data.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lottery.Service
{
    public class ExcelManager : IExcelManager
    {
        private readonly IRepository<Code> _codeRepository;

        public ExcelManager(IRepository<Code> codeRepository)
        {
            _codeRepository = codeRepository;
        }

        public void ProcessExcelPackage(FileInfo fileInfo)
        {
            using (var content = new ExcelPackage(fileInfo))
            {
                ProcessExcelPackage(fileInfo);
            }
        }

        public void ProcessExcelPackage(Stream stream)
        {
            using (var content = new ExcelPackage(stream))
            {
                ProcessExcelPackage(stream);
            }
        }

        private void ProcessExcelPackage(ExcelPackage content)
        {
            var worksheet = content.Workbook.Worksheets[1];
            var numberOfCodes = worksheet.Dimension.Rows;

            for (int i = 1; i <= numberOfCodes; i++)
            {
                var code = worksheet.Cells[i, 1].Value.ToString();
                var isWinning = bool.Parse(worksheet.Cells[i, 2].Value.ToString());

                if (!_codeRepository.GetAll().Any(x => x.CodeValue == code))
                {
                    var codeObject = new Code
                    {
                        CodeValue = code,
                        IsWinning = isWinning
                    };

                    _codeRepository.Insert(codeObject);
                    Console.WriteLine("Code inserted.");
                }
            }
        }
    }
}
