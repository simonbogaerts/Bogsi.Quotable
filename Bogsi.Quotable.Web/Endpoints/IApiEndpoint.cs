// -----------------------------------------------------------------------
// <copyright file="IApiEndpoint.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Endpoints;

/// <summary>
/// Interface used during route creation.
/// </summary>
public interface IApiEndpoint
{
    /// <summary>
    /// Provide a discoverable endpoint. During startup these are found through reflection.
    /// </summary>
    /// <param name="endpoints">Endpoint collection.</param>
    void MapRoute(IEndpointRouteBuilder endpoints);
}
