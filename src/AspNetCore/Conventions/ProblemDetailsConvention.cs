﻿using FluentValidation;
using FluentValidation.Results;
using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Rocket.Surgery.Conventions;
using Rocket.Surgery.Conventions.DependencyInjection;
using Rocket.Surgery.LaunchPad.AspNetCore.Validation;
using Rocket.Surgery.LaunchPad.Foundation;

namespace Rocket.Surgery.LaunchPad.AspNetCore.Conventions;

/// <summary>
///     ProblemDetailsConvention.
///     Implements the <see cref="IServiceConvention" />
/// </summary>
/// <seealso cref="IServiceConvention" />
/// <seealso cref="IServiceConvention" />
[PublicAPI]
[ExportConvention]
[AfterConvention(typeof(AspNetCoreConvention))]
public class ProblemDetailsConvention : IServiceConvention
{
    /// <inheritdoc />
    public void Register(IConventionContext context, IConfiguration configuration, IServiceCollection services)
    {
        services
           .AddProblemDetails()
           .AddProblemDetailsConventions();

        services.AddOptions<ApiBehaviorOptions>()
                .Configure(options => options.SuppressModelStateInvalidFilter = true);
        services.AddOptions<ProblemDetailsOptions>()
                .Configure<IOptions<ApiBehaviorOptions>>(
                     (builder, apiBehaviorOptions) =>
                     {
                         var currentIncludeExceptionDetails = builder.IncludeExceptionDetails;
                         builder.IncludeExceptionDetails = (httpContext, exception) =>
                             exception is not IProblemDetailsData && currentIncludeExceptionDetails(httpContext, exception);
                         builder.OnBeforeWriteDetails = (_, problemDetails) =>
                         {
                             if (
                                 !problemDetails.Status.HasValue
                              || !apiBehaviorOptions.Value.ClientErrorMapping.TryGetValue(problemDetails.Status.Value, out var clientErrorData)
                             )
                             {
                                 return;
                             }

                             problemDetails.Title ??= clientErrorData.Title;
                             problemDetails.Type ??= clientErrorData.Link;
                         };
//                         builder.MapToProblemDetailsDataException<NotFoundException>(StatusCodes.Status404NotFound);
//                         builder.MapToProblemDetailsDataException<RequestFailedException>(StatusCodes.Status400BadRequest);
//                         builder.MapToProblemDetailsDataException<NotAuthorizedException>(StatusCodes.Status403Forbidden);
                         builder.Map<ValidationException>(
                             exception => new FluentValidationProblemDetails(exception.Errors)
                             {
                                 Status = StatusCodes.Status422UnprocessableEntity
                             }
                         );
                         builder.Map<Exception>(
                             (context, ex) => ex is not IProblemDetailsData && context.Items[typeof(ValidationResult)] is ValidationResult,
                             (context, _) =>
                             {
                                 var result = context.Items[typeof(ValidationResult)] as ValidationResult;
                                 return new FluentValidationProblemDetails(result!.Errors)
                                 {
                                     Status = StatusCodes.Status422UnprocessableEntity
                                 };
                             }
                         );
                     }
                 );
    }
}
