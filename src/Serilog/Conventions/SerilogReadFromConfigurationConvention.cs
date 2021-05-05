using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.Configuration;
using Rocket.Surgery.LaunchPad.Serilog.Conventions;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.Collections.Generic;

[assembly: Convention(typeof(SerilogReadFromConfigurationConvention))]

namespace Rocket.Surgery.LaunchPad.Serilog.Conventions
{
    /// <summary>
    /// SerilogReadFromConfigurationConvention.
    /// Implements the <see cref="ISerilogConvention" />
    /// </summary>
    /// <seealso cref="ISerilogConvention" />
    [LiveConvention]
    public class SerilogReadFromConfigurationConvention : ISerilogConvention, IConfigurationConvention
    {
        /// <inheritdoc />
        public void Register([NotNull] IConventionContext context, IConfiguration configuration, IConfigurationBuilder builder)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var applicationLogLevel = configuration.GetValue<LogLevel?>("ApplicationState:LogLevel");
            if (applicationLogLevel.HasValue)
            {
                builder.AddInMemoryCollection(
                    new Dictionary<string, string>
                    {
                        {
                            "Serilog:MinimumLevel:Default",
                            LevelConvert.ToSerilogLevel(applicationLogLevel.Value).ToString()
                        }
                    }
                );
            }
        }

        /// <inheritdoc />
        public void Register(
            [NotNull] IConventionContext context,
            IServiceProvider services,
            IConfiguration configuration,
            LoggerConfiguration loggerConfiguration
        )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            loggerConfiguration.ReadFrom.Configuration(configuration);
        }
    }
}