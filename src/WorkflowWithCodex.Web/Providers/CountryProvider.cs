using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.Web.Providers;

public sealed class CountryProvider : IDataSourceProvider
{
    public string Key => "Countries";

    public Task<IReadOnlyList<SelectOption>> GetOptionsAsync(DynamicFormState context, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<SelectOption> options =
        [
            new("IR", "Iran"),
            new("DE", "Germany"),
            new("US", "United States")
        ];

        return Task.FromResult(options);
    }
}
