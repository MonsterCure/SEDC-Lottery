using Lottery.Data;
using Lottery.Data.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Lottery.Service
{
    public class AzureCodesManager : ICodesManager
    {
        private readonly IExcelManager _excelManager;

        public AzureCodesManager(IExcelManager excelManager)
        {
            _excelManager = excelManager;
        }

        public void ProcessCodes()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(""); // TODO: set Azure BLOB path

            using (var stream = client.GetStreamAsync("codes.xlsx").Result)
            {
                _excelManager.ProcessExcelPackage(stream);
            }

            client.Dispose();
        }
    }
}
