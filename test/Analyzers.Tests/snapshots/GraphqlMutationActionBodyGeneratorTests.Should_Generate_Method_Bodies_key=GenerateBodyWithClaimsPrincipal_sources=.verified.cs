﻿//HintName: Rocket.Surgery.LaunchPad.Analyzers/Rocket.Surgery.LaunchPad.Analyzers.GraphqlMutationActionBodyGenerator/RocketMutation_Methods.cs
#nullable enable
using TestNamespace;
using System.Security.Claims;
using MediatR;

namespace MyNamespace.Controllers
{
    public partial class RocketMutation
    {
        public partial async Task<RocketModel> Save2Rocket(IMediator mediator, ClaimsPrincipal claimsPrincipal, Save2Rocket.Request request)
        {
            var result = await Mediator.Send(request with { ClaimsPrincipal = claimsPrincipal }).ConfigureAwait(false);
            return result;
        }
    }
}
#nullable restore
