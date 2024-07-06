﻿using System.Reflection;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Rocket.Surgery.LaunchPad.HotChocolate.FairyBread;

/// <summary>
///     Instructs FairyBread to not run any validators that
///     are implicitly associated with the annotated argument's type.
///     Explicit validators will still be run.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class DontValidateImplicitlyAttribute : ArgumentDescriptorAttribute
{
    protected override void OnConfigure(
        IDescriptorContext context,
        IArgumentDescriptor descriptor,
        ParameterInfo parameter
    )
    {
        descriptor.DontValidateImplicitly();
    }
}

public static class DontValidateImplicitlyArgumentDescriptorExtensions
{
    /// <summary>
    ///     Instructs FairyBread to not run any validators that
    ///     are implicitly associated with this argument's runtime type.
    ///     Explicit validators will still be run.
    /// </summary>
    public static IArgumentDescriptor DontValidateImplicitly(
        this IArgumentDescriptor descriptor
    )
    {
        descriptor.Extend().OnBeforeNaming((completionContext, argDef) => { argDef.ContextData[WellKnownContextData.DontValidateImplicitly] = true; });

        return descriptor;
    }
}