using Bogsi.Quotable.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var application = builder.Build();
application.ConfigureWebApplication();

application.Run();
