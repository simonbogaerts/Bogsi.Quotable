// -----------------------------------------------------------------------
// <copyright file="WebApplicationExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions;

using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Serilog;

/// <summary>
/// Configure the WebApplication and request pipeline.
/// </summary>
internal static class WebApplicationExtensions
{
    /// <summary>
    /// Configuration order and usings.
    /// </summary>
    /// <param name="application">WebApplication.</param>
    internal static void ConfigureWebApplication(this WebApplication application)
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
