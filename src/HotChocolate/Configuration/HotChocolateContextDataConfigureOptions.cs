﻿using HotChocolate;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.Options;
using Rocket.Surgery.LaunchPad.HotChocolate.Configuration;

namespace Rocket.Surgery.LaunchPad.HotChocolate.Conventions
{
    class HotChocolateContextDataConfigureOptions : IConfigureNamedOptions<RequestExecutorSetup>
    {
        public void Configure(RequestExecutorSetup options) => Configure(Options.DefaultName, options);

        public void Configure(string name, RequestExecutorSetup options)
        {
            options.SchemaBuilderActions.Add(new SchemaBuilderAction((provider, builder) => builder.SetContextData("SchemaName", name)));
            options.SchemaBuilderActions.Add(new SchemaBuilderAction(
                (provider, builder) =>
                {
                    builder
                       .TryAddSchemaInterceptor<ConfigureRootTypeSchemaInterceptor>()
                       .TryAddTypeInterceptor<NestedTypeNameTypeInterceptor>();
                }));
        }
    }
}