using System.Text.Json.Serialization;

namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public sealed class VisibilityCondition
{
    public required string Field { get; init; }
    [JsonPropertyName("equals")]
    public string? EqualsValue { get; init; }
    [JsonPropertyName("notEquals")]
    public string? NotEquals { get; init; }
    [JsonPropertyName("contains")]
    public string? Contains { get; init; }
    [JsonPropertyName("hasValue")]
    public bool? HasValue { get; init; }
    [JsonPropertyName("isEmpty")]
    public bool? IsEmpty { get; init; }
}
