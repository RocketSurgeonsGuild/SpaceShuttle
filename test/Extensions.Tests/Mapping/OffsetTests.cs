using AutoMapper;
using FluentAssertions;
using NodaTime;
using Xunit;
using Xunit.Abstractions;

namespace Extensions.Tests.Mapping;

public class OffsetTests : TypeConverterTest<OffsetTests.Converters>
{
    [Fact]
    public void ValidateMapping()
    {
        Config.AssertConfigurationIsValid();
    }

    [Fact]
    public void MapsFrom()
    {
        var mapper = Config.CreateMapper();

        var foo = new Foo1
        {
            Bar = Offset.FromHours(11)
        };

        var result = mapper.Map<Foo3>(foo).Bar;
        result.Should().Be(foo.Bar.ToTimeSpan());
    }

    [Fact]
    public void MapsTo()
    {
        var mapper = Config.CreateMapper();

        var foo = new Foo3
        {
            Bar = TimeSpan.FromHours(10)
        };

        var result = mapper.Map<Foo1>(foo).Bar;
        result.Should().Be(Offset.FromTimeSpan(foo.Bar));
    }

    public OffsetTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override void Configure(IMapperConfigurationExpression expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        expression.CreateMap<Foo1, Foo3>().ReverseMap();
    }

    private class Foo1
    {
        public Offset Bar { get; set; }
    }

    private class Foo3
    {
        public TimeSpan Bar { get; set; }
    }

    public class Converters : TypeConverterFactory
    {
        public override IEnumerable<Type> GetTypeConverters()
        {
            yield return typeof(ITypeConverter<Offset, TimeSpan>);
            yield return typeof(ITypeConverter<Offset?, TimeSpan?>);
            yield return typeof(ITypeConverter<TimeSpan, Offset>);
            yield return typeof(ITypeConverter<TimeSpan?, Offset?>);
        }
    }
}
