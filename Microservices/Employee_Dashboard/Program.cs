using Employee_Dashboard;
using Employee_Dashboard.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var url = builder.Configuration.GetValue<string>("BaseAPIUrl");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
