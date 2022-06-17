using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Hectagon.Models;

namespace Hectagon
{
    public static class PostJournalEntry
    {
        [FunctionName("PostJournalEntry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try{
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            Book book = new Book()
            {
                Name = data?.name,
                Author = data?.author,
                Readings = new System.Collections.Generic.List<Reading>()
            };

            String readingDateString  = data?.date;
            Reading reading = new Reading()
            {
                ReadingDate = DateOnly.Parse(readingDateString)
            };

            book.Readings.Add(reading);            

            if(string.IsNullOrEmpty(book.Name) ||
            string.IsNullOrEmpty(book.Author))
                return new BadRequestResult();

            string responseMessage = $"The book you read is called '{book.Name}', written by '{book.Author}' and you read it on {book.Readings[0].ReadingDate}.";

            return new OkObjectResult(responseMessage);
            } catch (FormatException e)
            {
                return new BadRequestObjectResult($"Incorrectly formatted date.\n\n{e.Message}");
            }
            catch (Exception e)
            {
                return new BadRequestResult();
            }
        }
    }
}
