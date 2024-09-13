using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Mappings.Resolvers;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Mappings;

public sealed class QuoteMappingProfiles : Profile
{
    public QuoteMappingProfiles()
    {
        GeneralMapping();
        RequestMapping();
        ResponseMapping();
    }

    private void GeneralMapping()
    {
        CreateMap<QuoteEntity, Quote>();
        CreateMap<Quote, QuoteEntity>();
    }

    private void RequestMapping()
    {
        CreateMap<GetQuotesParameters, GetQuotesHandlerRequest>()
            .ForMember(
                dest => dest.PageNumber,
                opt => opt.MapFrom(src => src.PageNumber == null || src.PageNumber < GetQuotesParameters.MinimumValue
                    ? GetQuotesParameters.DefaultPageNumber
                    : src.PageNumber))
            .ForMember(
                dest => dest.PageSize,
                opt => opt.MapFrom<PageSizeResolver>());
    }

    private void ResponseMapping()
    {
        CreateMap<Quote, QuoteResponseHandler>();
        CreateMap<QuoteResponseHandler, QuoteResponseContract>();
        CreateMap<GetQuotesHandlerResponse, GetQuotesResponse>();
    }
}
