using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public static class ServiceHelper
    {
        public static async Task<T> ClientApi<T>(string url, HttpMethod httpMethod, object body)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url)
                };

                if (body != null)
                    request.Content = new StringContent(JsonConvert.SerializeObject(body),
                        Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Processed" + url);
                    return JsonConvert.DeserializeObject<T>(res);
                }
                else
                {
                    Console.WriteLine("Failed to process" + url);
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to process" + url);
            }

            return default;
        }
    }
}
