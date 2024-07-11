using Amazon.Runtime;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using OnBoarding.api.Dto;
using Polly.CircuitBreaker;
using Polly.Registry;
using Polly.Retry;

namespace OnBoarding.api.Helper.Clients
{
    public interface IMovieApiClient
    {
        Task<Root> GetMovieByCity(string city);
    }

    public class MovieApiClient : IMovieApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReadOnlyPolicyRegistry<string> policyRegistry;

        public MovieApiClient(HttpClient client, IHttpContextAccessor httpContextAccessor, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _httpClient = client;
            this.httpContextAccessor = httpContextAccessor;
            this.policyRegistry = policyRegistry;
        }

        public async Task<Root> GetMovieByCity(string city)
        {
            try
            {
                Root result = null;
                //request.Headers.Add("Bearer", await httpContextAccessor.HttpContext.GetTokenAsync("access_token"));

                var retryPolicy = policyRegistry.Get<AsyncRetryPolicy>("httpretrypolicy");
                var policyResponse = await retryPolicy.ExecuteAndCaptureAsync(async () =>
                {
                    //var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/cinemamovie/moviebycity?City={city}");
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api.publicapis.org/entries")
                    };

                    var response = await _httpClient.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<Root>(responseString);
                    }
                    else
                    {
                        throw new Exception();
                    }
                });


                if (policyResponse.Outcome == Polly.OutcomeType.Successful)
                {
                    return result;
                }
                else
                {
                    throw policyResponse.FinalException;
                }

            }
            catch (Exception)
            {

            }

            return null;
        }

    }
}
