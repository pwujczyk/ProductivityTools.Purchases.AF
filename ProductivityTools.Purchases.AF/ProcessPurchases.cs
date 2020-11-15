using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductivityTools.Purchases.Contract;
using ProductivityTools.SimpleHttpPostClient;

namespace ProductivityTools.Purchases.AF
{
    public static  class ProcessPurchases
    {
        [FunctionName("ProcessPurchases")]
        public static async Task Run([ServiceBusTrigger("allegroquene", Connection = "PTPurchase")]Purchase purchase, ILogger log)
        {
            log.LogInformation($"Purchase: {purchase}");

            HttpPostClient client = new HttpPostClient(enableLogging:true);
            client.SetBaseUrl("http://localhost:58197");

            try
            {
                var result1 = await client.PostAsync<int>("Purchase", "Add", purchase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
