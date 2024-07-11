using Employee_Dashboard.Model;
using Newtonsoft.Json;
using System.Text;

namespace Employee_Dashboard.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmployeeService> logger;
        private readonly string? baseServerUrl;

        public EmployeeService(HttpClient httpClient, ILogger<EmployeeService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            this.logger = logger;
            baseServerUrl = configuration.GetSection("BaseAPIUrl").Value;
        }

        public async Task Create(string firstname, string lastName, string email)
        {
            try
            {
                var content = JsonConvert.SerializeObject(new Employee { FirstName = firstname, LastName = lastName, Email = email });
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/OnBoarding/api/v1", body);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting employees");
            }
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var response = await _httpClient.GetAsync($"/OnBoarding/api/v1");
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var order = JsonConvert.DeserializeObject<ApiResult<IEnumerable<Employee>>>(content);
                    return order.Data;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while getting employees");
            }

            return employees;
        }

        public async Task Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/OnBoarding/api/v1?id={id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("error");
            }
        }
    }
}
