using Lottery.Data;
using Lottery.Data.Model;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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

        public async void ProcessCodes()
        {
            //var client = new HttpClient();
            //client.BaseAddress = new Uri(""); // TODO: set Azure BLOB path

            //using (var stream = client.GetStreamAsync("codes.xlsx").Result)
            //{
            //    _excelManager.ProcessExcelPackage(stream);
            //}

            //client.Dispose();

            // but, if access key is private, we need to use SDK
            var cloudStorageAccount = CloudStorageAccount.Parse("--connectionString--");

            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var cloudContainer = cloudBlobClient.GetContainerReference("codefiles");

            var filesList = await cloudContainer.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.None, 10, new BlobContinuationToken(), null, null);

            foreach (var listBlobItem in filesList.Results)
            {
                var blobItem = (CloudBlockBlob)listBlobItem;
                var fileBlob = cloudContainer.GetBlockBlobReference(blobItem.Name);

                var stream = new MemoryStream();
                await fileBlob.DownloadToStreamAsync(stream);

                _excelManager.ProcessExcelPackage(stream);

                await fileBlob.DeleteAsync();
            }
        }
    }
}
