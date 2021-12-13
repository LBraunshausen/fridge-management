using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace fridge_management.Services
{
    /// <summary>
    /// HttpRequestService contains methods for http api requests
    /// </summary>
    public class HttpRequestService
    {
        /// <summary>
        /// Static method which calls the ean code api.
        /// </summary>
        /// <param name="ean">contains string which contains the ean code</param>
        /// <returns>JObject which contains the product info of the given ean</returns>
        public static JObject getItemByEan(string ean)
        {
            string url = "https://api.barcodelookup.com/v3/products";
            string key = "646red6gh2dtletavgcn981205sywe";
         
            // create http connection
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            string urlParameters = $"?barcode={ean}&formatted=y&key={key}";

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObject = response.Content.ReadAsAsync<JObject>().Result;                
                return dataObject;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }            
        }
    }
}
