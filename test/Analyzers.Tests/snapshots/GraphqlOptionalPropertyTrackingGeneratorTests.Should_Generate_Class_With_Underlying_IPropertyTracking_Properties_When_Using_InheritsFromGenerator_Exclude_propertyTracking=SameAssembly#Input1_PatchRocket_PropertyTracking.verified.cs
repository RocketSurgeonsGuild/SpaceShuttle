﻿//HintName: Rocket.Surgery.LaunchPad.Analyzers/Rocket.Surgery.LaunchPad.Analyzers.PropertyTrackingGenerator/Input1_PatchRocket_PropertyTracking.cs
#nullable enable
using NodaTime;
using System;

namespace Sample.Core.Operations.Rockets
{
    [System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class PatchRocket
    {
        public Rocket.Surgery.LaunchPad.Foundation.Assigned<Instant> PlannedDate { get; set; } = Rocket.Surgery.LaunchPad.Foundation.Assigned<Instant>.Empty(default);
        public Rocket.Surgery.LaunchPad.Foundation.Assigned<int> Type { get; set; } = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(default);

#pragma warning disable CA1034
        public record Changes
        {
            public bool PlannedDate { get; init; }
            public bool Type { get; init; }
        }

        public Changes GetChangedState()
        {
            return new Changes()
            {
                PlannedDate = PlannedDate.HasBeenSet(),
                Type = Type.HasBeenSet()
            };
        }

        public global::Request ApplyChanges(global::Request state)
        {
            if (PlannedDate.HasBeenSet())
            {
                state.PlannedDate = PlannedDate!;
            }

            if (Type.HasBeenSet())
            {
                state.Type = Type!;
            }

            ResetChanges();
            return state;
        }

        public PatchRocket ResetChanges()
        {
            PlannedDate = Rocket.Surgery.LaunchPad.Foundation.Assigned<Instant>.Empty(PlannedDate);
            Type = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(Type);
            return this;
        }

        void IPropertyTracking<global::Request>.ResetChanges()
        {
            ResetChanges();
        }

        public static global::Sample.Core.Operations.Rockets.PatchRocket TrackChanges(global::Request value, global::System.Guid id) => new global::Sample.Core.Operations.Rockets.PatchRocket()
        {
            Id = id,
            PlannedDate = Rocket.Surgery.LaunchPad.Foundation.Assigned<Instant>.Empty(value.PlannedDate),
            Type = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(value.Type)
        };
    }
}
#nullable restore
