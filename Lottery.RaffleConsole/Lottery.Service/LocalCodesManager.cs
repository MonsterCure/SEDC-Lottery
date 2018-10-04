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
    public class LocalCodesManager : ICodesManager
    {
        private readonly IRepository<Code> _codeRepository;
        private readonly IExcelManager _excelManager;

        public LocalCodesManager(IRepository<Code> codeRepository, IExcelManager excelManager)
        {
            _codeRepository = codeRepository;
            _excelManager = excelManager;
        }

        public void ProcessCodes()
        {
            var folderName = @"CodeFiles\";
            var folderPath = $@"{Directory.GetCurrentDirectory()}\{folderName}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var directoryInfo = new DirectoryInfo(folderPath);
            var files = directoryInfo.GetFiles("*.xlsx");

            foreach (var file in files)
            {
                _excelManager.ProcessExcelPackage(file);
                //using (var content = new ExcelPackage(file))
                //{
                //    var worksheet = content.Workbook.Worksheets[1];
                //    var numberOfCodes = worksheet.Dimension.Rows;

                //    for (int i = 1; i <= numberOfCodes; i++)
                //    {
                //        var code = worksheet.Cells[i, 1].Value.ToString();
                //        var isWinning = bool.Parse(worksheet.Cells[i, 2].Value.ToString());

                //        if (!_codeRepository.GetAll().Any(x => x.CodeValue == code))
                //        {
                //            var codeObject = new Code
                //            {
                //                CodeValue = code,
                //                IsWinning = isWinning
                //            };

                //            _codeRepository.Insert(codeObject);
                //            Console.WriteLine("Code inserted.");
                //        }
                //    }
                //}

                File.Delete(file.FullName);
            }
        }
    }
}
