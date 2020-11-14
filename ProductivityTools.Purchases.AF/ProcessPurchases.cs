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

    //public class HttpPostClient
    //{
    //    private string BaseUrl { get; set; }
    //    public HttpClient HttpClient { get; private set; }

    //    public HttpPostClient()
    //    {
    //        this.HttpClient = new HttpClient();
    //    }

    //    public HttpPostClient(bool enableLogging)
    //    {
    //        if (enableLogging)
    //        {
    //            this.HttpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
    //        }
    //        else
    //        {
    //            this.HttpClient = new HttpClient();
    //        }
    //    }

    //    public void SetBaseUrl(string url)
    //    {
    //        this.BaseUrl = url;
    //    }

    //    public T PostAsync<T>(string controller, string action, object obj)
    //    {
    //        Uri url = new Uri(BaseUrl + "/" + controller + "/" + action);
    //        this.HttpClient.DefaultRequestHeaders.Accept.Clear();
    //        this.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //        //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, action);

    //        var dataAsString = JsonConvert.SerializeObject(obj);
    //        var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

    //        HttpResponseMessage response = this.HttpClient.PostAsync(url, content).Result; ;
    //        if (response.IsSuccessStatusCode)
    //        {
    //            var resultAsString = response.Content.ReadAsStringAsync().Result;
    //            T result = JsonConvert.DeserializeObject<T>(resultAsString);
    //            return result;
    //        }
    //        throw new Exception(response.ReasonPhrase);
    //    }
    //}
}
