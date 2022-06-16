using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hectagon
{
    public static class PostJournalEntry
    {
        [FunctionName("PostJournalEntry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string dateRead = data?.date;
            string author = data?.author;

            if(string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(dateRead) ||
            string.IsNullOrEmpty(author))
                return new BadRequestResult();

            string responseMessage = $"The book you read is called '{name}', written by '{author}' and you read it on {dateRead}.";

            return new OkObjectResult(responseMessage);
        }
    }
}
