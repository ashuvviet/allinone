using Core.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: AutoGenerate]
namespace Core.Contracts
{
    public interface IWebService : IService
    {
        Task<T> ClientApi<T>(string url, HttpMethod httpMethod, object body);
    }

    [Service(Contract = typeof(WebService))]
    public class WebService : IWebService
    {
        public string Name => "WebService";

        private ILoggingService Logger = Container.Resolve<ILoggingService>();

        public async Task<T> ClientApi<T>(string url, HttpMethod httpMethod, object body)
        {
            var client = new HttpClient();
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(url)
                };

                if (body != null)
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(body),
                        Encoding.UTF8, "application/json");
                }

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
            catch (HttpRequestException ex)
            {
                Logger.Log($"exception during accessing url {url}, details : {ex.Message}");
                throw;
            }
            catch (WebException ex)
            {
                Logger.Log(ex.Message);
                throw;
            }
        }
    }
}
