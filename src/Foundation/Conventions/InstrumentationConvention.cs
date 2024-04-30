using Microsoft.Extensions.Configuration;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.LaunchPad.Telemetry;

namespace Rocket.Surgery.LaunchPad.Foundation.Conventions;

/// <summary>
///     InstrumentationConvention.
///     Implements <see cref="IOpenTelemetryConvention" /> and <see cref="IOpenTelemetryConvention" />
/// </summary>
/// <seealso cref="IOpenTelemetryConvention" />
[PublicAPI]
[ExportConvention]
public class InstrumentationConvention : IOpenTelemetryConvention
{
    /// <inheritdoc />
    public void Register(IConventionContext context, IConfiguration configuration, IOpenTelemetryBuilder builder)
    {
        builder.WithTracing(b => b.AddHttpClientInstrumentation(x => x.RecordException = true));
        builder.WithMetrics(b => b.AddRuntimeInstrumentation().AddHttpClientInstrumentation());
    }
}