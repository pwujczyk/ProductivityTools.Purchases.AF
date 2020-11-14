using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using ProductivityTools.Purchases.Contract;

namespace ProductivityTools.Purchases.AF
{
    public static class ProcessPurchases
    {
        [FunctionName("ProcessPurchases")]
        public static void Run([ServiceBusTrigger("allegroquene", Connection = "PTPurchase")]Purchase purchase, ILogger log)
        {
            log.LogInformation($"Purchase: {purchase}");
        }
    }
}
