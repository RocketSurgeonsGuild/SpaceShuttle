﻿//HintName: Rocket.Surgery.LaunchPad.Analyzers/Rocket.Surgery.LaunchPad.Analyzers.ControllerActionBodyGenerator/RocketController_Methods.cs
#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Rocket.Surgery.LaunchPad.AspNetCore;
using TestNamespace;
using FluentValidation.AspNetCore;
using Rocket.Surgery.LaunchPad.AspNetCore.Validation;

namespace MyNamespace.Controllers
{
    public partial class RocketController
    {
        [ProducesDefaultResponseType]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(FluentValidationProblemDetails), 422)]
        public partial async Task<ActionResult<RocketModel>> Save2Rocket(Guid id, string? sn, [Bind("Other")] Save2Rocket.Request request)
        {
            var result = await Mediator.Send(request with { Id = id, Sn = sn, R = this.Request }, HttpContext.RequestAborted).ConfigureAwait(false);
            return new ObjectResult(result)
            {
                StatusCode = 200
            };
        }
    }
}
#nullable restore