using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NodaTime;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.LaunchPad.Foundation.Conventions;
using System.Text.Json;
using System.Text.Json.Serialization;

[assembly: Convention(typeof(NewtonsoftJsonConvention))]

namespace Rocket.Surgery.LaunchPad.Foundation.Conventions
{
    public class NewtonsoftJsonConvention : IServiceConvention
    {
        public void Register(IConventionContext context, IConfiguration configuration, IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<JsonSerializerSettings>>(
                _ =>
                    new ConfigureNamedOptions<JsonSerializerSettings>(
                        null,
                        options => options.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()))
                    )
            );
            services.AddTransient<IPostConfigureOptions<JsonSerializerSettings>>(
                sp =>
                    new PostConfigureOptions<JsonSerializerSettings, IDateTimeZoneProvider>(
                        null,
                        sp.GetRequiredService<IDateTimeZoneProvider>(),
                        (options, provider) => options.ConfigureNodaTimeForLaunchPad(provider)
                    )
            );
        }
    }
}