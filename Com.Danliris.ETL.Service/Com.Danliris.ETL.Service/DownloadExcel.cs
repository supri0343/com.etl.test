using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Com.Danliris.ETL.Service.Models;
using Com.Danliris.ETL.Service.Services.Interfaces;


namespace Com.Danliris.ETL.Service
{
    public class DownloadExcel
    {
        private readonly IDownloadExcelPrintingService _downloadExcelPrintingService;

        public DownloadExcel(
            IDownloadExcelPrintingService downloadExcelPrintingService
            )
        {
            _downloadExcelPrintingService = downloadExcelPrintingService;
            //httpClient = clientFactory.CreateClient();
        }

        [FunctionName("DownloadExcel")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            try{
                var periode = DateTime.Now;
                var excel = _downloadExcelPrintingService.Download(periode);
                excel.Position = 0;

                FileStreamResult fileStreamResult = new FileStreamResult(excel, "application/excel");
                fileStreamResult.FileDownloadName = $"Printing.xlsx";

                return fileStreamResult;

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new ResponseFailed("Gagal mengakses aplikasi", ex));
            }

        }
    }
}
