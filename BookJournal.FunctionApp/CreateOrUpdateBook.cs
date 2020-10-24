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
using System.ComponentModel;
using BookJournal.FunctionApp.Exceptions;

namespace BookJournal.FunctionApp
{
    public static class CreateOrUpdateBook
    {
        [FunctionName("CreateOrUpdateBook")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "BookJournal",
            collectionName: "Books",
            ConnectionStringSetting = "CosmosDbConnection")] out dynamic cosmosDoc,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            cosmosDoc = null;

            try
            {
                var inputBook = JsonConvert.DeserializeObject<Book>(requestBody);
                ValidateBook(inputBook);
                if(inputBook.Id == Guid.Empty)
                {
                    inputBook.Id = Guid.NewGuid();
                }
                cosmosDoc = inputBook;
                log.LogInformation($"Recieved information for book: {inputBook.Name}");
                return new OkObjectResult(inputBook);
            }
            catch (BookNameNullException)
            {
                log.LogError($"No name passed for book");
                return new BadRequestObjectResult($"Could not parse name in body");
            }
            catch(NoUpnException)
            {
                log.LogError($"No upn passed for book");
                return new BadRequestObjectResult($"Could not parse upn in body");
            }
            catch (Exception)
            {
                log.LogError($"Could not parse book from body: {requestBody}");
                return new BadRequestObjectResult($"Could not parse book information.  Please pass book information as a json string.");
            }
        }

        private static bool ValidateBook(Book book)
        {
            if (book.Name is null)
                throw new BookNameNullException();
            if (book.Upn is null)
                throw new NoUpnException();
            
            return true;
        }

    }

    
}
