// -----------------------------------------------------------------------
// <copyright file="WebApplicationModules.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules;

using Bogsi.Quotable.Modules.Extensions;

using Microsoft.AspNetCore.Builder;

/// <summary>
/// Configure the WebApplicationBuilder to not spoil the Program.cs.
/// </summary>
internal static class WebApplicationModules
{
    /// <summary>
    /// Add and configure the WebApplicationBuilder.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void ConfigureModules(this WebApplicationBuilder builder)
    {
        builder.AddAndConfigureConfigSingletons();
        builder.AddAndConfigureSerilogAndSeq();
        builder.AddAndConfigureHealthChecks();
        builder.AddAndConfigureAuth();
        builder.AddAndConfigureDatabaseContexts();
        builder.AddAndConfigureMediatr();
        builder.AddAndConfigureMassTransit();
        builder.AddAndConfigureDistributedCache();
        builder.AddAndConfigureApiExplorerWithVersioning();
        builder.AddAndConfigureSwaggerGenWithAuth();
        builder.AddAndConfigureServices();
    }
}
