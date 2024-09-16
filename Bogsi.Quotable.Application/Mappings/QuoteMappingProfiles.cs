using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Contracts.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuoteById;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteById;
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
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom<PageNumberResolver>())
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom<PageSizeResolver>());
        CreateMap<CreateQuoteRequest, CreateQuoteHandlerRequest>();
        CreateMap<CreateQuoteHandlerRequest, Quote>()
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom<PublicIdResolver>());
    }

    private void ResponseMapping()
    {
        CreateMap<Quote, QuoteResponseHandler>();
        CreateMap<QuoteResponseHandler, QuoteResponseContract>();
        CreateMap<GetQuotesHandlerResponse, GetQuotesResponse>();
        CreateMap<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>();
        CreateMap<Quote, CreateQuoteHandlerResponse>();
        CreateMap<CreateQuoteHandlerResponse, QuoteResponseContract>();
    }
}
