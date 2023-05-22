﻿using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.Options;

namespace Rocket.Surgery.LaunchPad.HotChocolate.Configuration;

internal class HotChocolateContextDataConfigureOptions : IConfigureNamedOptions<RequestExecutorSetup>
{
    public void Configure(RequestExecutorSetup options)
    {
        Configure(Options.DefaultName, options);
    }

    public void Configure(string name, RequestExecutorSetup options)
    {
        options.OnConfigureSchemaBuilderHooks.Add(
            new OnConfigureSchemaBuilderAction((context, services) => context.SchemaBuilder.SetContextData("SchemaName", name))
        );
        options.OnConfigureSchemaBuilderHooks.Add(
            new OnConfigureSchemaBuilderAction((context, services) => context.SchemaBuilder.TryAddTypeInterceptor(typeof(NestedTypeNameTypeInterceptor)))
        );
    }
}
