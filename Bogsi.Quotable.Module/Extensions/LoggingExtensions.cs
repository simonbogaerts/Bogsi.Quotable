// -----------------------------------------------------------------------
// <copyright file="LoggingExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Modules.Extensions;

using Microsoft.AspNetCore.Builder;

using Serilog;

/// <summary>
/// Extensions regarding logging.
/// </summary>
internal static class LoggingExtensions
{
    /// <summary>
    /// Configure and serilog and sinks.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void AddAndConfigureSerilogAndSeq(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
    }
}
