using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.LaunchPad.Spatial.Conventions;

[assembly: Convention(typeof(SpatialNewtonsoftJsonConvention))]
namespace Rocket.Surgery.LaunchPad.Spatial.Conventions
{
    public class SpatialNewtonsoftJsonConvention : IServiceConvention
    {
        public void Register(IConventionContext context, IConfiguration configuration, IServiceCollection services)
        {
            services.TryAddSingleton(_ => NtsGeometryServices.Instance.CreateGeometryFactory(4326));
            services
               .AddOptions<JsonSerializerSettings>(null)
               .Configure<GeometryFactory>(
                    (options, factory) => options.ConfigureNetTopologySuiteForLaunchPad(factory)
                );
        }
    }
}