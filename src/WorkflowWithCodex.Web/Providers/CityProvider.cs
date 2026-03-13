using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.Web.Providers;

public sealed class CityProvider : IDataSourceProvider
{
    public string Key => "Cities";

    public Task<IReadOnlyList<SelectOption>> GetOptionsAsync(DynamicFormState context, CancellationToken cancellationToken = default)
    {
        var country = context["CountryId"]?.ToString();

        IReadOnlyList<SelectOption> options = country switch
        {
            "IR" => [new("THR", "Tehran"), new("MHD", "Mashhad")],
            "DE" => [new("BER", "Berlin"), new("MUC", "Munich")],
            "US" => [new("NYC", "New York"), new("SFO", "San Francisco")],
            _ => []
        };

        return Task.FromResult(options);
    }
}
