using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Services;

public sealed class OptionService(IEnumerable<IDataSourceProvider> providers)
{
    private readonly Dictionary<string, IDataSourceProvider> _providers = providers.ToDictionary(provider => provider.Key, StringComparer.OrdinalIgnoreCase);

    public async Task<IReadOnlyList<SelectOption>> GetOptionsAsync(string dataSourceKey, DynamicFormState context, CancellationToken cancellationToken = default)
    {
        if (!_providers.TryGetValue(dataSourceKey, out var provider))
        {
            throw new InvalidOperationException($"No data source provider was found for key '{dataSourceKey}'.");
        }

        return await provider.GetOptionsAsync(context, cancellationToken);
    }
}
