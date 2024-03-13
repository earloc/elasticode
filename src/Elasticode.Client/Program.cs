using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .Configure<AppOptions>(options => builder.Configuration.GetSection("App").Bind(options))
;

builder.Services.AddKeyedScoped("api", (services, key) => {
    var options = services.GetRequiredService<IOptions<AppOptions>>();
    return new HttpClient() {
        BaseAddress = new Uri(options.Value.BaseUrl)
    };
});

builder.Services.AddScoped(services => new CodeClient("", services.GetRequiredKeyedService<HttpClient>("api")));


await builder.Build().RunAsync();
