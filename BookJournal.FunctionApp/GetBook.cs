using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BookJournal.FunctionApp.Models;

namespace BookJournal.FunctionApp
{
    public static class GetBook
    {
        [FunctionName("GetBook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB(
            databaseName: "BookJournal",
            collectionName: "Books",
            ConnectionStringSetting = "CosmosDbConnection",
            Id = "{Query.id}",
            PartitionKey = "{Query.upn}")]Book book,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if(book == null)
            {
                log.LogInformation($"Book not found for id={req.Query["id"]}");
                return new NotFoundResult();
            } else
            {
                log.LogInformation($"Found book {book.Name}");
            }

            return new OkObjectResult(book);
        }
    }
}
