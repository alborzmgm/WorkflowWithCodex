using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Providers;

public interface IDataSourceProvider
{
    string Key { get; }
    Task<IReadOnlyList<SelectOption>> GetOptionsAsync(DynamicFormState context, CancellationToken cancellationToken = default);
}
