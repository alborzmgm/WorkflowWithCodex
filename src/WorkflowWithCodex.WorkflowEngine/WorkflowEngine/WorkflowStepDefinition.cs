namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public sealed class WorkflowStepDefinition
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public List<FieldDefinition> Fields { get; init; } = [];
}
