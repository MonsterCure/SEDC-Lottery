using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lottery.Service
{
    public interface IExcelManager
    {
        void ProcessExcelPackage(FileInfo fileInfo);

        void ProcessExcelPackage(Stream stream);
    }
}
