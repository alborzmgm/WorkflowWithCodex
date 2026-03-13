namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public static class VisibilityEvaluator
{
    public static bool IsVisible(object? value, VisibilityCondition? condition)
    {
        if (condition is null)
        {
            return true;
        }

        var stringValue = value?.ToString();

        if (condition.HasValue.HasValue)
        {
            return condition.HasValue.Value == HasValue(value);
        }

        if (condition.IsEmpty.HasValue)
        {
            return condition.IsEmpty.Value == !HasValue(value);
        }

        if (!string.IsNullOrWhiteSpace(condition.Contains))
        {
            return Contains(value, condition.Contains);
        }

        if (!string.IsNullOrWhiteSpace(condition.NotEquals))
        {
            return !string.Equals(stringValue, condition.NotEquals, StringComparison.OrdinalIgnoreCase);
        }

        if (!string.IsNullOrWhiteSpace(condition.EqualsValue))
        {
            return string.Equals(stringValue, condition.EqualsValue, StringComparison.OrdinalIgnoreCase);
        }

        return true;
    }

    private static bool Contains(object? value, string expected)
    {
        if (value is IEnumerable<string> values)
        {
            return values.Contains(expected, StringComparer.OrdinalIgnoreCase);
        }

        return value?.ToString()?.Contains(expected, StringComparison.OrdinalIgnoreCase) == true;
    }

    private static bool HasValue(object? value)
    {
        return value switch
        {
            null => false,
            string text => !string.IsNullOrWhiteSpace(text),
            IEnumerable<string> values => values.Any(),
            _ => true
        };
    }
}
