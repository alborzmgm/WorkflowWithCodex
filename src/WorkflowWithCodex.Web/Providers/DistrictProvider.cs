using WorkflowWithCodex.WorkflowEngine.Model;
using WorkflowWithCodex.WorkflowEngine.Providers;
using WorkflowWithCodex.WorkflowEngine.WorkflowEngine;

namespace WorkflowWithCodex.Web.Providers;

public sealed class DistrictProvider : IDataSourceProvider
{
    public string Key => "Districts";

    public Task<IReadOnlyList<SelectOption>> GetOptionsAsync(DynamicFormState context, CancellationToken cancellationToken = default)
    {
        var city = context["CityId"]?.ToString();

        IReadOnlyList<SelectOption> options = city switch
        {
            "THR" => [new("THR1", "District 1"), new("THR2", "District 2")],
            "MHD" => [new("MHD1", "Vakilabad"), new("MHD2", "Sajjad")],
            "BER" => [new("BER1", "Mitte"), new("BER2", "Kreuzberg")],
            "MUC" => [new("MUC1", "Altstadt"), new("MUC2", "Schwabing")],
            "NYC" => [new("NYC1", "Manhattan"), new("NYC2", "Brooklyn")],
            "SFO" => [new("SFO1", "Sunset"), new("SFO2", "Mission")],
            _ => []
        };

        return Task.FromResult(options);
    }
}
