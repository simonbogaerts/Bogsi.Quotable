// -----------------------------------------------------------------------
// <copyright file="RequestContextLoggingExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions.DetailedExtensions;

using Bogsi.Quotable.Web.Extensions.Middleware;

/// <summary>
/// Extensions regarding context logging middleware.
/// </summary>
internal static class RequestContextLoggingExtensions
{
    /// <summary>
    /// Use correlation id middleware.
    /// </summary>
    /// <param name="application">WebApplication.</param>
    /// <returns>Web application wit added middleware.</returns>
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder application)
    {
        application.UseMiddleware<RequestContextLoggingMiddleware>();

        return application;
    }
}
