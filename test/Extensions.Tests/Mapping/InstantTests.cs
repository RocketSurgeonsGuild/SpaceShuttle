using Extensions.Tests.Mapping.Helpers;
using Microsoft.Extensions.Time.Testing;
using NodaTime;
using Riok.Mapperly.Abstractions;
using Rocket.Surgery.Extensions.Testing;
using Rocket.Surgery.LaunchPad.Mapping;
using Rocket.Surgery.LaunchPad.Mapping.Profiles;

namespace Extensions.Tests.Mapping;

public partial class InstantTests(ITestOutputHelper testOutputHelper) : AutoFakeTest(testOutputHelper)
{
    FakeTimeProvider _fakeTimeProvider = new ();

    private class Foo1
    {
        public Instant Bar { get; set; }
    }

    private class Foo2
    {
        public Instant? Bar { get; set; }
    }

    private class Foo3
    {
        public DateTime Bar { get; set; }
    }

    private class Foo4
    {
        public DateTime? Bar { get; set; }
    }

    private class Foo5
    {
        public DateTimeOffset Bar { get; set; }
    }

    private class Foo6
    {
        public DateTimeOffset? Bar { get; set; }
    }

    [Theory, MapperData<Mapper>]
    public Task TestsMapper(MethodResult result)
    {
        return Verify(result.Map(
            new Mapper(),
            _fakeTimeProvider.GetUtcNow(),
            _fakeTimeProvider.GetUtcNow().UtcDateTime,
            Instant.FromDateTimeOffset(_fakeTimeProvider.GetUtcNow())
        )).UseHashedParameters(result.ToString())
          .DontScrubDateTimes();
    }

    [Mapper, PublicAPI]
    [UseStaticMapper(typeof(DateTimeMapper))]
    [UseStaticMapper(typeof(NodaTimeMapper))]
    [UseStaticMapper(typeof(NodaTimeDateTimeMapper))]
    private partial class Mapper
    {
        public partial Foo1 MapFoo1(Foo2 source);
        public partial Foo1 MapFoo1(Foo3 source);
        public partial Foo1 MapFoo1(Foo4 source);
        public partial Foo1 MapFoo1(Foo5 source);
        public partial Foo1 MapFoo1(Foo6 source);

        public partial Foo2 MapFoo2(Foo1 source);
        public partial Foo2 MapFoo2(Foo3 source);
        public partial Foo2 MapFoo2(Foo4 source);
        public partial Foo2 MapFoo2(Foo5 source);
        public partial Foo2 MapFoo2(Foo6 source);

        public partial Foo3 MapFoo3(Foo1 source);
        public partial Foo3 MapFoo3(Foo2 source);
        public partial Foo3 MapFoo3(Foo4 source);
        public partial Foo3 MapFoo3(Foo5 source);
        public partial Foo3 MapFoo3(Foo6 source);

        public partial Foo4 MapFoo4(Foo1 source);
        public partial Foo4 MapFoo4(Foo2 source);
        public partial Foo4 MapFoo4(Foo3 source);
        public partial Foo4 MapFoo4(Foo5 source);
        public partial Foo4 MapFoo4(Foo6 source);

        public partial Foo5 MapFoo5(Foo1 source);
        public partial Foo5 MapFoo5(Foo2 source);
        public partial Foo5 MapFoo5(Foo3 source);
        public partial Foo5 MapFoo5(Foo4 source);
        public partial Foo5 MapFoo5(Foo6 source);

        public partial Foo6 MapFoo6(Foo1 source);
        public partial Foo6 MapFoo6(Foo2 source);
        public partial Foo6 MapFoo6(Foo3 source);
        public partial Foo6 MapFoo6(Foo4 source);
        public partial Foo6 MapFoo6(Foo5 source);
    }
}
