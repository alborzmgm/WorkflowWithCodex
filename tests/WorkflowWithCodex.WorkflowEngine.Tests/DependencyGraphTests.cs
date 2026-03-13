using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Tests;

public sealed class DependencyGraphTests
{
    [Fact]
    public void GetDescendants_ReturnsTransitiveDependents()
    {
        var definition = new WorkflowDefinition
        {
            Id = "wf",
            Title = "Workflow",
            Steps =
            [
                new WorkflowStepDefinition
                {
                    Id = "s1",
                    Title = "Step 1",
                    Fields =
                    [
                        new FieldDefinition { Key = "CountryId", Label = "Country", Type = FieldType.Select },
                        new FieldDefinition { Key = "CityId", Label = "City", Type = FieldType.Select, DependsOn = ["CountryId"] },
                        new FieldDefinition { Key = "DistrictId", Label = "District", Type = FieldType.Select, DependsOn = ["CityId"] }
                    ]
                }
            ]
        };

        var graph = new DependencyGraph(definition);

        var descendants = graph.GetDescendants("CountryId");

        Assert.Contains("CityId", descendants);
        Assert.Contains("DistrictId", descendants);
        Assert.Equal(2, descendants.Count);
    }

    [Fact]
    public void GetDependents_ReturnsImmediateDependentsOnly()
    {
        var definition = new WorkflowDefinition
        {
            Id = "wf",
            Title = "Workflow",
            Steps =
            [
                new WorkflowStepDefinition
                {
                    Id = "s1",
                    Title = "Step 1",
                    Fields =
                    [
                        new FieldDefinition { Key = "Parent", Label = "Parent", Type = FieldType.Text },
                        new FieldDefinition { Key = "Child", Label = "Child", Type = FieldType.Text, DependsOn = ["Parent"] },
                        new FieldDefinition { Key = "GrandChild", Label = "GrandChild", Type = FieldType.Text, DependsOn = ["Child"] }
                    ]
                }
            ]
        };

        var graph = new DependencyGraph(definition);

        var dependents = graph.GetDependents("Parent");

        Assert.Single(dependents);
        Assert.Contains("Child", dependents);
    }
}
