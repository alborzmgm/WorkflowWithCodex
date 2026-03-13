using System.Text.Json.Serialization;

namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public sealed class VisibilityCondition
{
    public required string Field { get; init; }
    [JsonPropertyName("equals")]
    public required string ExpectedValue { get; init; }
}
