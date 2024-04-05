﻿//HintName: Rocket.Surgery.LaunchPad.Analyzers/Rocket.Surgery.LaunchPad.Analyzers.GraphqlOptionalPropertyTrackingGenerator/Input2_GetRocket_TrackingRequest_Optionals.cs
#nullable enable
using System.Security.Claims;
using System;

namespace TestNamespace
{
    public static partial class GetRocket
    {
        [System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public partial record TrackingRequest
        {
            public Guid Id { get; set; }
            public ClaimsPrincipal? ClaimsPrincipal { get; set; }
            public HotChocolate.Optional<string?> Name { get; set; }

            public global::TestNamespace.GetRocket.PatchRequest Create()
            {
                var value = new global::TestNamespace.GetRocket.PatchRequest(Id, ClaimsPrincipal)
                {
                };
                if (Name.HasValue)
                {
                    value = value with
                    {
                        Name = Name.Value
                    };
                }

                return value;
            }
        }
    }
}
#nullable restore
