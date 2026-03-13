using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.WorkflowEngine.Tests;

public sealed class FieldValidatorTests
{
    [Fact]
    public void RequiredField_ReturnsError_WhenMissing()
    {
        var field = new FieldDefinition { Key = "Email", Label = "Email", Type = FieldType.Text, Required = true };

        var error = FieldValidator.GetValidationMessage(field, null);

        Assert.Equal("Email is required.", error);
    }

    [Fact]
    public void EmailValidation_ReturnsError_ForInvalidEmail()
    {
        var field = new FieldDefinition
        {
            Key = "Email",
            Label = "Email",
            Type = FieldType.Text,
            Required = true,
            Validation = new FieldValidationRule { Format = "email" }
        };

        var error = FieldValidator.GetValidationMessage(field, "invalid-email");

        Assert.Equal("Please enter a valid email address.", error);
    }

    [Fact]
    public void EmailValidation_ReturnsNoError_ForValidEmail()
    {
        var field = new FieldDefinition
        {
            Key = "Email",
            Label = "Email",
            Type = FieldType.Text,
            Required = true,
            Validation = new FieldValidationRule { Format = "email" }
        };

        var error = FieldValidator.GetValidationMessage(field, "user@example.com");

        Assert.Null(error);
    }
}
