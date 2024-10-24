// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Bogsi.Quotable.Modules;
using Bogsi.Quotable.Web.Endpoints;
using Bogsi.Quotable.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureModules();

builder.ConfigureApiEndpoints();

var application = builder.Build();

application.ConfigureRequestPipeline();

await application.RunAsync().ConfigureAwait(false);

/// <summary>
/// The partial class is required for integration testing (devcontainer setup).
/// </summary>
public partial class Program
{
}
