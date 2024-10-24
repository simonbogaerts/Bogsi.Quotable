// -----------------------------------------------------------------------
// <copyright file="RequestContextLoggingMiddleware.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Middleware;

using Microsoft.Extensions.Primitives;

using Serilog.Context;

/// <summary>
/// Middle ware to add a correlation id to the HttpContext.
/// </summary>
/// <param name="next">delegate too continue execution.</param>
public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next = next;

    /// <summary>
    /// Execute middleware logic.
    /// </summary>
    /// <param name="context">HttpContext of the request.</param>
    /// <returns>Task.</returns>
    public Task Invoke(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        string correlationId = GetCorrelationId(context);

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return _next.Invoke(context);
        }
    }

    /// <summary>
    /// Get or add a correlation id to the HttpContext.
    /// </summary>
    /// <param name="context">HttpContext of the request.</param>
    /// <returns>The correlation id.</returns>
    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);

        return correlationId.FirstOrDefault() ?? Guid.NewGuid().ToString();
    }
}

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
