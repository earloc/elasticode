using Elasticode.Components;
using Elasticode.Client;

const string baseUrl = "http://localhost:5231";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddKeyedScoped("api", (key, services) => 
    new HttpClient() {
        BaseAddress = new Uri(baseUrl)
    }
);

builder.Services.AddScoped(services => new CodeClient(baseUrl, services.GetRequiredKeyedService<HttpClient>("api")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}{
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
