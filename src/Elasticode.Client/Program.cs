using Elasticode.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var appSection = builder.Configuration.GetSection("App");

builder.Services
    .Configure<AppOptions>(options => appSection.Bind(options))
;

builder.Services.AddKeyedScoped("api", (services, key) => {
    var options = services.GetRequiredService<IOptions<AppOptions>>();
    return new HttpClient() {
        BaseAddress = new Uri(options.Value.BaseUrl)
    };
});
builder.Services.AddScoped(services => new ModuleClient("", services.GetRequiredKeyedService<HttpClient>("api")));

builder.Services.AddScoped<AppViewModel>();


await builder.Build().RunAsync();
