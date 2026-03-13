namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public enum FieldType
{
    Text,
    Number,
    Select,
    Checkbox,
    CheckboxList
}

public sealed class FieldDefinition
{
    public required string Key { get; init; }
    public required string Label { get; init; }
    public required FieldType Type { get; init; }
    public string? Placeholder { get; init; }
    public string? DataSource { get; init; }
    public bool Required { get; init; }
    public FieldValidationRule? Validation { get; init; }
    public List<string> DependsOn { get; init; } = [];
    public VisibilityCondition? VisibleWhen { get; init; }
}
