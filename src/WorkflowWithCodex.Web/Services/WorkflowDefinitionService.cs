using WorkflowWithCodex.WorkflowEngine.Services;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.Web.Services;

public sealed class WorkflowDefinitionService(IWebHostEnvironment environment)
{
    private readonly WorkflowDefinitionLoader _loader = new();

    public WorkflowDefinition GetWorkflow()
    {
        var workflowPath = Path.Combine(environment.ContentRootPath, "WorkflowDefinitions", "location-workflow.json");
        var json = File.ReadAllText(workflowPath);
        return _loader.Load(json);
    }
}
