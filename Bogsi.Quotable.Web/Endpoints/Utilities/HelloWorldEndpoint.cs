// -----------------------------------------------------------------------
// <copyright file="HelloWorldEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints.Utilities;

/// <summary>
/// Hello world endpoint.
/// Test endpoint used furing development.
/// </summary>
internal class HelloWorldEndpoint : IApiEndpoint
{
    /// <inheritdoc/>
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!.Equals(Constants.Environments.Production, StringComparison.Ordinal))
        {
            return;
        }

        endpoints
            .MapGet("utilities/hello-world", HelloWorld)
            .WithTags(Constants.Endpoints.Utilities)
            .Produces(StatusCodes.Status200OK)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    /// <summary>
    /// Endpoint logic.
    /// </summary>
    /// <returns>Hello world string.</returns>
    internal static IResult HelloWorld()
    {
        return Results.Ok("Hello, Quotable!");
    }
}
