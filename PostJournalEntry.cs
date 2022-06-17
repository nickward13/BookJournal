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
        public static ActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "BookJournal",
                collectionName: "Books",
                ConnectionStringSetting = "CosmosDBConnection"
            )]out dynamic book,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            book = new Book()
            {
                UserId = "12345"
            };

            try{
            
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            book.Name = (String)data?.name;
            book.Author = (String)data?.author;
            book.Readings = new System.Collections.Generic.List<Reading>();

            String readingDateString  = (String)data?.date;
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
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
