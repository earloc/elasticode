using Elasticode.Client;
using Elasticode.Client.Pages;
using Elasticode.Components;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();

var appSection = builder.Configuration.GetSection("App");
builder.Services.Configure<AppOptions>(appSection);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

builder.Services.AddKeyedScoped("api", (services, key) => {
    var options = services.GetRequiredService<IOptions<AppOptions>>();
    return new HttpClient() {
        BaseAddress = new Uri(options.Value.BaseUrl)
    };
});

builder.Services.AddScoped(services => new ModuleClient("", services.GetRequiredKeyedService<HttpClient>("api")));
builder.Services.AddScoped<AppViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseOpenApi();
    app.UseSwaggerUi();
    app.UseReDoc(options => {
        options.Path = "/redoc";
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Modules).Assembly);

app.MapControllers();

app.Run();
