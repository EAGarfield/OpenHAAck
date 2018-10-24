using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class GetRatings
    {

        [FunctionName("GetRatings")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "GetRatings/userId/{userId}")] HttpRequestMessage req,
            [DocumentDB(databaseName: "erinntestcosmos",
                        collectionName: "Erinntest",
                 ConnectionStringSetting = "Cosmosconnection",
           SqlQuery = "SELECT top 10 * FROM c WHERE c.userId={userId} ")]
             IEnumerable<dynamic> data, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            if (data == null)
            {

                return req.CreateResponse(HttpStatusCode.BadRequest, "N/a");
            }
            try
            {
                var finaluserid = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(finaluserid, System.Text.Encoding.UTF8, "application/json") };
            }
            catch
            {
            return req.CreateResponse(HttpStatusCode.OK);

        }
        }
    }
}
