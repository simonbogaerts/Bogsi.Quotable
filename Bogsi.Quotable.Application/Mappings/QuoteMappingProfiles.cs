﻿using AutoMapper;
using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Mappings.Resolvers;
using Bogsi.Quotable.Application.Models;

namespace Bogsi.Quotable.Application.Mappings;

public sealed class QuoteMappingProfiles : Profile
{
    public QuoteMappingProfiles()
    {
        AllowNullCollections = true;

        GeneralMapping();
        RequestMapping();
        ResponseMapping();
    }

    private void GeneralMapping()
    {
        CreateMap<QuoteEntity, Quote>();
        CreateMap<Quote, QuoteEntity>()
            .ForMember(dest => dest.Created, opt => opt.MapFrom<CreatedDateResolver>())
            .ForMember(dest => dest.Updated, opt => opt.MapFrom<UpdatedDateResolver>());
    }

    private void RequestMapping()
    {
        // Get Quotes
        CreateMap<GetQuotesParameters, GetQuotesHandlerRequest>()
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom<PageNumberResolver>())
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom<PageSizeResolver>());

        // Create Quote
        CreateMap<CreateQuoteRequest, CreateQuoteHandlerRequest>();
        CreateMap<CreateQuoteHandlerRequest, Quote>()
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom<NewPublicIdResolver>());

        // Update Quote
        CreateMap<UpdateQuoteRequest, UpdateQuoteHandlerRequest>()
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom<PassPublicIdResolver>());
        CreateMap<UpdateQuoteHandlerRequest, Quote>();
    }

    private void ResponseMapping()
    {
        // Get Quotes
        CreateMap<Quote, GetQuotesSingleQuoteHandlerResponse>();
        CreateMap<GetQuotesSingleQuoteHandlerResponse, GetQuotesSingleQuoteResponse>();

        // Get Quote By Id 
        CreateMap<Quote, GetQuoteByIdHandlerResponse>();
        CreateMap<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>();

        // Create Quote 
        CreateMap<Quote, CreateQuoteHandlerResponse>();
        CreateMap<CreateQuoteHandlerResponse, CreateQuoteResponse>();
    }
}
