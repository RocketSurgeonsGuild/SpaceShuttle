#if NET6_0_OR_GREATER
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.LaunchPad.Foundation;
using Rocket.Surgery.LaunchPad.Spatial;
using Serilog;
using Serilog.Context;

namespace Extensions.Tests;

[UsesVerify]
public class SerilogDestructuringTests : LoggerTest
{
    [Fact]
    public async Task Should_Destructure_Sjt_Values_JsonElement()
    {
        using var _ = CaptureLogs(out var logs);

        Logger.LogInformation(
            "This is just a test {@Data}",
            JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(new { test = true, system = new { data = "1234" } }), options: null)
        );

        await Verify(logs.Select(z => z.RenderMessage()));
    }

    [Fact]
    public async Task Should_Destructure_Sjt_Values_JsonDocument()
    {
        using var _ = CaptureLogs(out var logs);

        Logger.LogInformation(
            "This is just a test {@Data}",
            JsonSerializer.Deserialize<JsonDocument>(JsonSerializer.Serialize(new { test = true, system = new { data = "1234" } }), options: null)
        );

        await Verify(logs.Select(z => z.RenderMessage()));
    }

    [Fact]
    public async Task Should_Destructure_NewtonsoftJson_JObject()
    {
        using var _ = CaptureLogs(out var logs);

        Logger.LogInformation("This is just a test {@Data}", JObject.FromObject(new { test = true, system = new { data = "1234" } }));

        await Verify(logs.Select(z => z.RenderMessage()));
    }

    [Fact]
    public async Task Should_Destructure_NewtonsoftJson_JArray()
    {
        using var _ = CaptureLogs(out var logs);

        Logger.LogInformation("This is just a test {@Data}", JArray.FromObject(new object[] { 1, "2", 3d }));

        await Verify(logs.Select(z => z.RenderMessage()));
    }

    [Fact]
    public async Task Should_Destructure_NewtonsoftJson_JValue()
    {
        var faker = new Faker
        {
            Random = new Randomizer(17)
        };
        using var _ = CaptureLogs(out var logs);

        Logger.LogInformation("This is just a test {@Data}", new JValue(faker.Random.Guid()));

        await Verify(logs.Select(z => z.RenderMessage()));
    }

    [Fact]
    public async Task Should_Destructure_NetTopologySuite_AttributesTable()
    {
        var value = new FeatureFactory().CreateRandomAttributes(
            ( "id", TypeCode.Int32 ),
            ( "label", TypeCode.String ), ( "number1", TypeCode.Double ),
            ( "number2", TypeCode.Int64 )
        );

        using var _ = CaptureLogs(out var logs);
        Logger.LogInformation("This is just a test {@Data}", value);
        await Verify(logs.Select(z => z.RenderMessage()));
    }

    public SerilogDestructuringTests(ITestOutputHelper outputHelper) : base(
        outputHelper, LogLevel.Information, configureLogger: configuration => configuration
                                                                             .Destructure.NewtonsoftJsonTypes()
                                                                             .Destructure.SystemTextJsonTypes()
                                                                             .Destructure.NetTopologySuiteTypes()
    )
    {
        _outputHelper = outputHelper;

        LogContext.PushProperty("SourceContext", nameof(SerilogDestructuringTests));
    }

    private readonly ITestOutputHelper _outputHelper;

    [Theory]
    [InlineData(OgcGeometryType.Point, 5, false)]
    [InlineData(OgcGeometryType.Point, 5, true)]
    [InlineData(OgcGeometryType.LineString, 5, false)]
    [InlineData(OgcGeometryType.LineString, 5, true)]
    [InlineData(OgcGeometryType.Polygon, 5, false)]
    [InlineData(OgcGeometryType.Polygon, 5, true)]
    [InlineData(OgcGeometryType.MultiPoint, 5, false)]
    [InlineData(OgcGeometryType.MultiPoint, 5, true)]
    [InlineData(OgcGeometryType.MultiLineString, 5, false)]
    [InlineData(OgcGeometryType.MultiLineString, 5, true)]
    [InlineData(OgcGeometryType.MultiPolygon, 5, false)]
//    [InlineData(OgcGeometryType.MultiPolygon, 5, true)]
    public async Task Should_Destructure_NetTopologySuite_FeatureCollection(OgcGeometryType type, int num, bool threeD)
    {
        var fc = new FeatureCollection();
        for (var i = 0; i < num; i++)
        {
            fc.Add(
                FeatureFactory.Create(
                    type, threeD, ( "id", TypeCode.Int32 ),
                    ( "label", TypeCode.String ), ( "number1", TypeCode.Double ),
                    ( "number2", TypeCode.Int64 )
                )
            );
        }

        using var _ = CaptureLogs(out var logs);
        Logger.LogInformation("This is just a test {@Data}", fc);
        await Verify(logs.Select(z => z.RenderMessage())).UseParameters(type, num, threeD);
    }

    [Theory]
    [InlineData(OgcGeometryType.Point, false)]
    [InlineData(OgcGeometryType.Point, true)]
    [InlineData(OgcGeometryType.LineString, false)]
    [InlineData(OgcGeometryType.LineString, true)]
    [InlineData(OgcGeometryType.Polygon, false)]
    [InlineData(OgcGeometryType.Polygon, true)]
    [InlineData(OgcGeometryType.MultiPoint, false)]
    [InlineData(OgcGeometryType.MultiPoint, true)]
    [InlineData(OgcGeometryType.MultiLineString, false)]
    [InlineData(OgcGeometryType.MultiLineString, true)]
    [InlineData(OgcGeometryType.MultiPolygon, false)]
//    [InlineData(OgcGeometryType.MultiPolygon, true)]
    public async Task Should_Destructure_NetTopologySuite_Geometry(OgcGeometryType type, bool threeD)
    {
        var geometry = new FeatureFactory().CreateRandomGeometry(type, threeD);

        using var _ = CaptureLogs(out var logs);
        Logger.LogInformation("This is just a test {@Data}", geometry);
        await Verify(logs.Select(z => z.RenderMessage())).UseParameters(type, threeD);
    }
}


#endif
