namespace WorkflowWithCodex.WorkflowEngine.Model;

public sealed class DynamicFormState
{
    private readonly Dictionary<string, object?> _values = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, object?> Values => _values;

    public object? this[string key]
    {
        get => _values.TryGetValue(key, out var value) ? value : null;
        set => _values[key] = value;
    }

    public bool TryGetValue(string key, out object? value) => _values.TryGetValue(key, out value);

    public void Remove(string key) => _values.Remove(key);
}
