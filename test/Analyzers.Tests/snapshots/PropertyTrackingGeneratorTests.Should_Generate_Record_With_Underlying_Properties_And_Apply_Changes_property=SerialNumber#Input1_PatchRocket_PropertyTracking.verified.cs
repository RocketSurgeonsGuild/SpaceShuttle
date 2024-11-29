﻿//HintName: Rocket.Surgery.LaunchPad.Analyzers/Rocket.Surgery.LaunchPad.Analyzers.PropertyTrackingGenerator/Input1_PatchRocket_PropertyTracking.cs
#nullable enable
#pragma warning disable CA1002, CA1034, CA1822, CS0105, CS1573, CS8602, CS8603, CS8618, CS8669
using System;

[System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
public partial record PatchRocket
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public Rocket.Surgery.LaunchPad.Foundation.Assigned<Guid> Id { get; set; } = Rocket.Surgery.LaunchPad.Foundation.Assigned<Guid>.Empty(default);

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public Rocket.Surgery.LaunchPad.Foundation.Assigned<string> SerialNumber { get; set; } = Rocket.Surgery.LaunchPad.Foundation.Assigned<string>.Empty(default);

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public Rocket.Surgery.LaunchPad.Foundation.Assigned<int> Type { get; set; } = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(default);

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public record Changes
    {
        public bool Id { get; init; }
        public bool SerialNumber { get; init; }
        public bool Type { get; init; }
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public Changes GetChangedState()
    {
        return new Changes()
        {
            Id = Id.HasValue,
            SerialNumber = SerialNumber.HasValue,
            Type = Type.HasValue
        };
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public global::Request ApplyChanges(global::Request state)
    {
        if (Id.HasValue)
        {
            state = state with
            {
                Id = Id!
            };
        }

        if (SerialNumber.HasValue)
        {
            state = state with
            {
                SerialNumber = SerialNumber!
            };
        }

        if (Type.HasValue)
        {
            state = state with
            {
                Type = Type!
            };
        }

        ResetChanges();
        return state;
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public PatchRocket ResetChanges()
    {
        Id = Rocket.Surgery.LaunchPad.Foundation.Assigned<Guid>.Empty(Id);
        SerialNumber = Rocket.Surgery.LaunchPad.Foundation.Assigned<string>.Empty(SerialNumber);
        Type = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(Type);
        return this;
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    void IPropertyTracking<global::Request>.ResetChanges()
    {
        ResetChanges();
    }

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage, System.CodeDom.Compiler.GeneratedCode("Rocket.Surgery.LaunchPad.Analyzers", "version"), System.Runtime.CompilerServices.CompilerGenerated]
    public static global::PatchRocket TrackChanges(global::Request value) => new global::PatchRocket()
    {
        Id = Rocket.Surgery.LaunchPad.Foundation.Assigned<Guid>.Empty(value.Id),
        SerialNumber = Rocket.Surgery.LaunchPad.Foundation.Assigned<string>.Empty(value.SerialNumber),
        Type = Rocket.Surgery.LaunchPad.Foundation.Assigned<int>.Empty(value.Type)
    };
}
#pragma warning restore CA1002, CA1034, CA1822, CS0105, CS1573, CS8602, CS8603, CS8618, CS8669
#nullable restore
