// <copyright file="QuoteMappingProfiles.cs" company="BOGsi">
// Copyright (c) BOGsi. All rights reserved.
// </copyright>

namespace Bogsi.Quotable.Application.Mappings;

using AutoMapper;

using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Entities;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Mappings.Resolvers;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Application.Utilities;

/// <summary>
/// The different mapping profiles regarding the Quote models.
/// </summary>
public sealed class QuoteMappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteMappingProfiles"/> class.
    /// AutoMapper requires the profiles to be configured witin the ctor.
    /// </summary>
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
            .ForMember(dest => dest.Cursor, opt => opt.MapFrom<CursorResolver>())
            .ForMember(dest => dest.Size, opt => opt.MapFrom<SizeResolver>());

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
        CreateMap<CursorResponse<Quote>, GetQuotesHandlerResponse>();
        CreateMap<GetQuotesHandlerResponse, GetQuotesResponse>();

        // Get Quote By Id
        CreateMap<Quote, GetQuoteByIdHandlerResponse>();
        CreateMap<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>();

        // Create Quote
        CreateMap<Quote, CreateQuoteHandlerResponse>();
        CreateMap<CreateQuoteHandlerResponse, CreateQuoteResponse>();
    }
}
