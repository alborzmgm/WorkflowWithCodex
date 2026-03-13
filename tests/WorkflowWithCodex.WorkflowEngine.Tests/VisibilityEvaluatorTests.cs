using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Tests;

public sealed class VisibilityEvaluatorTests
{
    [Fact]
    public void EqualsCondition_MatchesCaseInsensitive()
    {
        var condition = new VisibilityCondition { Field = "Status", EqualsValue = "Active" };

        var isVisible = VisibilityEvaluator.IsVisible("active", condition);

        Assert.True(isVisible);
    }

    [Fact]
    public void NotEqualsCondition_HidesWhenSame()
    {
        var condition = new VisibilityCondition { Field = "Type", NotEquals = "Business" };

        var isVisible = VisibilityEvaluator.IsVisible("Business", condition);

        Assert.False(isVisible);
    }

    [Fact]
    public void ContainsCondition_WorksForStringLists()
    {
        var condition = new VisibilityCondition { Field = "Services", Contains = "Consulting" };

        var isVisible = VisibilityEvaluator.IsVisible(new[] { "Payroll", "Consulting" }, condition);

        Assert.True(isVisible);
    }

    [Fact]
    public void HasValueCondition_MatchesPresence()
    {
        var condition = new VisibilityCondition { Field = "CompanyName", HasValue = true };

        var isVisible = VisibilityEvaluator.IsVisible("Contoso", condition);

        Assert.True(isVisible);
    }

    [Fact]
    public void IsEmptyCondition_MatchesWhitespace()
    {
        var condition = new VisibilityCondition { Field = "Notes", IsEmpty = true };

        var isVisible = VisibilityEvaluator.IsVisible("   ", condition);

        Assert.True(isVisible);
    }
}
