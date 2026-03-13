using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.Web.Providers;

public sealed class ServiceCatalogProvider : IDataSourceProvider
{
    public string Key => "Services";

    public Task<IReadOnlyList<SelectOption>> GetOptionsAsync(DynamicFormState context, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<SelectOption> options =
        [
            new("Accounting", "Accounting"),
            new("Consulting", "Consulting"),
            new("Payroll", "Payroll")
        ];

        return Task.FromResult(options);
    }
}
