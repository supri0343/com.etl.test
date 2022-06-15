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
    public static class GetArea
    {
        [FunctionName("GetArea")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get List of Area");
            req.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:SQLConnectionString", EnvironmentVariableTarget.Process);

            var adapters = new AreaAdapters(connectionString);
            var result = await adapters.Get();
            if (adapters != null) {
                return new OkObjectResult(new ResponseSuccess("success", result));
            }

            return new BadRequestObjectResult("Failed");
        }
    }
}
