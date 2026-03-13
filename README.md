# WorkflowWithCodex

Dynamic workflow-driven form engine sample built with **.NET 10 + Blazor Server**.

## Run

```bash
cd /home/runner/work/WorkflowWithCodex/WorkflowWithCodex
DOTNET_CLI_TELEMETRY_OPTOUT=1 dotnet run --project /home/runner/work/WorkflowWithCodex/WorkflowWithCodex/src/WorkflowWithCodex.Web/WorkflowWithCodex.Web.csproj
```

Open the URL printed by `dotnet run`.

## Architecture

- `src/WorkflowWithCodex.WorkflowEngine/WorkflowEngine`
  - `WorkflowDefinition`, `WorkflowStepDefinition`, `FieldDefinition`
  - `DependencyGraph`
- `src/WorkflowWithCodex.WorkflowEngine/Providers`
  - `IDataSourceProvider`
- `src/WorkflowWithCodex.WorkflowEngine/Services`
  - `OptionService`, `WorkflowDefinitionLoader`
- `src/WorkflowWithCodex.Web/Providers`
  - `CountryProvider`, `CityProvider`, `DistrictProvider`, `ServiceCatalogProvider`
- `src/WorkflowWithCodex.Web/Components/BlazorComponents`
  - `DynamicForm` and dynamic field components rendered using `DynamicComponent`
- `src/WorkflowWithCodex.Web/WorkflowDefinitions/location-workflow.json`
  - Declarative workflow with 3 steps

## Workflow JSON Notes

- JSON defines steps, fields, dependencies (`dependsOn`), visibility (`visibleWhen`) and data source keys.
- `visibleWhen` supports `equals`, `notEquals`, `contains`, `hasValue`, and `isEmpty`.
- Optional field `validation` supports `format: email`.
- JSON does not include business logic, SQL, or API calls.
- C# provider classes implement dynamic option loading using current form context.
