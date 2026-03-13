namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public sealed class DependencyGraph
{
    private readonly Dictionary<string, HashSet<string>> _adjacency = new(StringComparer.OrdinalIgnoreCase);

    public DependencyGraph(WorkflowDefinition definition)
    {
        foreach (var field in definition.Steps.SelectMany(step => step.Fields))
        {
            if (!_adjacency.ContainsKey(field.Key))
            {
                _adjacency[field.Key] = [];
            }

            foreach (var parent in field.DependsOn)
            {
                if (!_adjacency.TryGetValue(parent, out var dependents))
                {
                    dependents = [];
                    _adjacency[parent] = dependents;
                }

                dependents.Add(field.Key);
            }
        }
    }

    public IReadOnlyCollection<string> GetDependents(string fieldKey)
    {
        return _adjacency.TryGetValue(fieldKey, out var dependents)
            ? dependents
            : [];
    }

    public IReadOnlyCollection<string> GetDescendants(string fieldKey)
    {
        var descendants = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var queue = new Queue<string>();

        queue.Enqueue(fieldKey);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!_adjacency.TryGetValue(current, out var children))
            {
                continue;
            }

            foreach (var child in children)
            {
                if (descendants.Add(child))
                {
                    queue.Enqueue(child);
                }
            }
        }

        return descendants;
    }
}
