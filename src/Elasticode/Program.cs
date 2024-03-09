using Elasticode.Components;
using Elasticode.Client;
using Elasticode;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddScoped<AppViewModel>();

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else 
{
    app.UseOpenApi();
    app.UseSwaggerUi();
    app.UseReDoc(options => {
        options.Path = "/redoc";
    });
}



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

app.Run();
