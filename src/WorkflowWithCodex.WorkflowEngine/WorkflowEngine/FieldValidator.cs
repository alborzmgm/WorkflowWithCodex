using System.Net.Mail;

namespace WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

public static class FieldValidator
{
    public static string? GetValidationMessage(FieldDefinition field, object? value)
    {
        if (field.Required && !HasValue(value))
        {
            return $"{field.Label} is required.";
        }

        if (field.Validation?.Format?.Equals("email", StringComparison.OrdinalIgnoreCase) == true && HasValue(value) && !IsValidEmail(value?.ToString()))
        {
            return "Please enter a valid email address.";
        }

        return null;
    }

    private static bool HasValue(object? value)
    {
        return value switch
        {
            null => false,
            string text => !string.IsNullOrWhiteSpace(text),
            IEnumerable<string> values => values.Any(),
            _ => true
        };
    }

    private static bool IsValidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var mailAddress = new MailAddress(email);
            return string.Equals(mailAddress.Address, email, StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
