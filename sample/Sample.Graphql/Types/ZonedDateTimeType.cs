using NodaTime;
using NodaTime.Text;
using Sample.Graphql.Extensions;
using Sample.Graphql.Helpers;
using System.Diagnostics.CodeAnalysis;

namespace Sample.Graphql.Types
{
    public class ZonedDateTimeType : StringToStructBaseType<ZonedDateTime>
    {
        private static string formatString = "uuuu'-'MM'-'dd'T'HH':'mm':'ss' 'z' 'o<g>";

        public ZonedDateTimeType()
            : base("ZonedDateTime")
        {
            Description =
                "A LocalDateTime in a specific time zone and with a particular offset to " +
                    "distinguish between otherwise-ambiguous instants.\n" +
                "A ZonedDateTime is global, in that it maps to a single Instant.";
        }

        protected override string Serialize(ZonedDateTime baseValue)
            => ZonedDateTimePattern
                .CreateWithInvariantCulture(formatString, DateTimeZoneProviders.Tzdb)
                .Format(baseValue);

        protected override bool TryDeserialize(string str, [NotNullWhen(true)] out ZonedDateTime? output)
            => ZonedDateTimePattern
                .CreateWithInvariantCulture(formatString, DateTimeZoneProviders.Tzdb)
                .TryParse(str, out output);
    }
}