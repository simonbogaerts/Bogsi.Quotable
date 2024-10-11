// -----------------------------------------------------------------------
// <copyright file="WebApplicationBuilderExtensions.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Bogsi.Quotable.Web.Extensions;

using Bogsi.Quotable.Web.Extensions.DetailedExtensions;

/// <summary>
/// Configure the WebApplicationBuilder to not spoil the Program.cs.
/// </summary>
internal static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Add and configure the WebApplicationBuilder.
    /// </summary>
    /// <param name="builder">WebApplicationBuilder during startip.</param>
    internal static void ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.AddLoggingWithSerilogAndSeq();
        builder.AddApiExplorerWithVersioning();
        builder.AddAuthenticationAndAuthorization();
        builder.AddSwaggerGenWithAuth();
        builder.AddApiEndpoints();
        builder.AddQuotableDbContext();
        builder.AddServices();
    }
}
