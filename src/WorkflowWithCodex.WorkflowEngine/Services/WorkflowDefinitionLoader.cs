using System.Text.Json;
using System.Text.Json.Serialization;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Services;

public sealed class WorkflowDefinitionLoader
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public WorkflowDefinition Load(string json)
    {
        var definition = JsonSerializer.Deserialize<WorkflowDefinition>(json, SerializerOptions);
        return definition ?? throw new InvalidOperationException("Workflow JSON could not be deserialized.");
    }
}
