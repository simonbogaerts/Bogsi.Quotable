using AutoMapper;
using Bogsi.Quotable.Application.Entities;
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
        
    }

    private void ResponseMapping()
    {

    }
}
