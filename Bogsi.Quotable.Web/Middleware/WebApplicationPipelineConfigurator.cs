// -----------------------------------------------------------------------
// <copyright file="WebApplicationPipelineConfigurator.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Middleware;

using Serilog;

/// <summary>
/// Configure the WebApplication and request pipeline.
/// </summary>
internal static class WebApplicationPipelineConfigurator
{
    /// <summary>
    /// Configuration order and usings.
    /// </summary>
    /// <param name="application">WebApplication.</param>
    internal static void ConfigureRequestPipeline(this WebApplication application)
    {
        application.UseRequestContextLogging();
        application.UseHttpsRedirection();
        application.UseSerilogRequestLogging();
        application.UseRouting();
        application.UseCors();
        application.UseAuthentication();
        application.UseAuthorization();
        application.UseVersionedApiEndpoints();
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}
