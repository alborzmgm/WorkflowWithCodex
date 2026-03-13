using WorkflowWithCodex.Web.Components;
using WorkflowWithCodex.Web.Providers;
using WorkflowWithCodex.Web.Services;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<WorkflowDefinitionService>();
builder.Services.AddSingleton<IDataSourceProvider, CountryProvider>();
builder.Services.AddSingleton<IDataSourceProvider, CityProvider>();
builder.Services.AddSingleton<IDataSourceProvider, DistrictProvider>();
builder.Services.AddSingleton<IDataSourceProvider, ServiceCatalogProvider>();
builder.Services.AddSingleton<OptionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
