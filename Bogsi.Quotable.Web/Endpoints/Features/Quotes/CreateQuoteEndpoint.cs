using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Contracts.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Bogsi.Quotable.Web.Endpoints.Features.Quotes;

public sealed class CreateQuoteEndpoint : IApiEndpoint
{
    public void MapRoute(IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapPost("quotes", CreateQuote)
            .WithTags(Constants.Endpoints.Quotes)
            .WithName(Constants.Endpoints.QuoteEndpoints.CreateQuoteEndpoint)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status409Conflict)
            .MapToApiVersion(1)
            .WithOpenApi();
    }

    internal static async Task<IResult> CreateQuote(
        [FromBody] CreateQuoteRequest newQuote,
        [FromServices] ICreateQuoteHandler createQuoteHandler,
        [FromServices] IValidator<CreateQuoteHandlerRequest> validator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = mapper.Map<CreateQuoteRequest, CreateQuoteHandlerRequest>(newQuote);

        var isValidRequest = await validator.ValidateAsync(request, cancellationToken);

        if (!isValidRequest.IsValid)
        {
            return Results.ValidationProblem(isValidRequest.ToDictionary());
        }

        var result = await createQuoteHandler.HandleAsync(request, cancellationToken);

        var response = mapper.Map<CreateQuoteHandlerResponse, QuoteResponseContract>(result);
        
        return Results.CreatedAtRoute(
            Constants.Endpoints.QuoteEndpoints.GetQuoteByIdEndpoint,
            new { id = response.PublicId }, 
            response);
    }
}
