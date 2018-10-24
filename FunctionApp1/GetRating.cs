using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class GetRating
    {
        
        [FunctionName("GetRating")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous,"get","post", Route = null)] HttpRequestMessage req,
            [DocumentDB(databaseName: "erinntestcosmos",
                        collectionName: "Erinntest",
                 ConnectionStringSetting = "Cosmosconnection",
                 Id = "{Query.id}")] dynamic ratingItem,
              TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            if (ratingItem == null)
            {

                return req.CreateResponse(HttpStatusCode.BadRequest,"N/a");
            }
            try
            {
                var newrating = Newtonsoft.Json.JsonConvert.SerializeObject(ratingItem);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(newrating, System.Text.Encoding.UTF8, "application/json") };
            }
            catch
            {
                return req.CreateResponse(HttpStatusCode.OK);
            }

        }
    }
}
