namespace Bogsi.Quotable.Web.Endpoints.Utilities;

internal class HelloWorldEndpoint : IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGet("utilities/hello-world", HelloWorld)
            .WithTags(Constants.Endpoints.Utilities)
            .Produces(StatusCodes.Status200OK)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    internal static IResult HelloWorld()
    {
        return Results.Ok("Hello, Quotable!");
    }
}
