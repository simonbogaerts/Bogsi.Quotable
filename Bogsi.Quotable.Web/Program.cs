using Bogsi.Quotable.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var application = builder.Build();
application.ConfigureWebApplication();

application.Run();

/// <summary>
/// The partial class is required for integration testing (devcontainer setup).
/// </summary>
public partial class Program { }