﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.LaunchPad.Grpc.Validation;

namespace Rocket.Surgery.LaunchPad.Grpc.Conventions;

/// <summary>
///     ProblemDetailsConvention.
///     Implements the <see cref="IServiceConvention" />
/// </summary>
/// <seealso cref="IServiceConvention" />
/// <seealso cref="IServiceConvention" />
[PublicAPI]
[ExportConvention]
[ConventionCategory(ConventionCategory.Application)]
public class GrpcConvention : IServiceConvention
{
    /// <inheritdoc />
    public void Register(IConventionContext context, IConfiguration configuration, IServiceCollection services)
    {
        services
           .AddGrpcValidation()
           .AddGrpc(
                options =>
                {
                    options.EnableMessageValidation();
                    options.Interceptors.Add<NotAuthorizedInterceptor>();
                    options.Interceptors.Add<NotFoundInterceptor>();
                    options.Interceptors.Add<RequestFailedInterceptor>();
                }
            )
            ;
    }
}
