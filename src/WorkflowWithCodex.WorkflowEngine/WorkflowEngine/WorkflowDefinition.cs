namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public sealed class WorkflowDefinition
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public List<WorkflowStepDefinition> Steps { get; init; } = [];
}
