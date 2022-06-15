using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Com.Danliris.ETL.Service.DBAdapters;
using Com.Danliris.ETL.Service.Models;

namespace Com.Danliris.ETL.Service
{
    public static class GetFilePeriode
    {
        [FunctionName("GetFilePeriode")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);
            int page;
            int size;
            var pageExtract = int.TryParse(req.Query["page"], out page);
            var sizeExtract = int.TryParse(req.Query["size"], out size);
            string order = req.Query["order"];
            if (order == null) {
                order = "{}";
            }
            string keyword = req.Query["keyword"];
            if (keyword == null) {
                keyword = "";
            }
            log.LogInformation(order + " "+ keyword);
            var adapters = new FilePeriodeAdapters(connectionString);
            var result = await adapters.Get(page, size, order, keyword);

            if (adapters != null) {
                
                return new OkObjectResult(new ResponseSuccess("success", result.data, result.info));
            }

            return new BadRequestObjectResult("Failed");
        }
    }
}
