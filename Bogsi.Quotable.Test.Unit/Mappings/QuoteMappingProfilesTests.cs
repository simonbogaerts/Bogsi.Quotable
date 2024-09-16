using Bogsi.Quotable.Application.Contracts.Quotes;
using Bogsi.Quotable.Application.Contracts.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuoteById;
using Bogsi.Quotable.Application.Contracts.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuoteById;
using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Mappings;

namespace Bogsi.Quotable.Test.Unit.Mappings;

public class QuoteMappingProfilesTests : TestBase<IMapper>
{
    #region Test Setup

    protected override IMapper Construct()
    {
        var configuration = new MapperConfiguration(x => x.AddProfile<QuoteMappingProfiles>());

        return configuration.CreateMapper();
    }

    #endregion

    #region General Mapping

    [Fact]
    public void GivenQuoteEntity_WhenMappingToModel_MapsFieldsCorrectly()
    {
        // Given
        QuoteEntity entity = new()
        {
            Id = 1,
            PublicId = Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<QuoteEntity, Quote>(entity);

        // THEN 
        result.Should().NotBeNull();
        result.PublicId.Should().Be(entity.PublicId);
        result.Created.Should().Be(entity.Created);
        result.Updated.Should().Be(entity.Updated);
        result.Value.Should().Be(entity.Value);
    }

    #endregion

    #region Request Mapping

    [Fact]
    public void GivenGetQuotesParameters_WhenAllPropertiesHaveAValue_ThenAllPropertiesAreMapped()
    {
        // GIVEN
        GetQuotesParameters parameters = new()
        {
            PageNumber = 1,
            PageSize = 10,
            Origin = "ORIGIN",
            Tag = "TAG",
            OrderBy = "ORDERBY",
            SearchQuery = "SEARCHQUERY",
            Fields = "FIELDS"
        };

        // WHEN 
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THEN 
        result.Should().NotBeNull();
        result.PageNumber.Should().Be(parameters.PageNumber);
        result.PageSize.Should().Be(parameters.PageSize);
        result.Origin.Should().Be(parameters.Origin);
        result.Tag.Should().Be(parameters.Tag);
        result.OrderBy.Should().Be(parameters.OrderBy);
        result.SearchQuery.Should().Be(parameters.SearchQuery);
        result.Fields.Should().Be(parameters.Fields);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(0, 0)]
    public void GivenGetQuotesParameters_WhenPageNumberOrPageSizeIsNullOrZero_ThenDefaultValuesAreProvided(int? pageNumber, int? pageSize)
    {
        // GIVEN
        GetQuotesParameters parameters = new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // WHEN
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THAN 
        result.Should().NotBeNull();
        result.PageNumber.Should().Be(GetQuotesParameters.DefaultPageNumber);
        result.PageSize.Should().Be(GetQuotesParameters.DefaultPageSize);
    }

    [Fact]
    public void GivenGetQuotesParameters_WhenPageSizeIsMoreThanMaximum_ThenMaximumValueIsProvided()
    {
        // GIVEN
        var wrongPageSize = GetQuotesParameters.MaximumPageSize + 1;

        GetQuotesParameters parameters = new()
        {
            PageSize = wrongPageSize
        };

        // WHEN
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THEN
        result.Should().NotBeNull();
        result.PageSize.Should().Be(GetQuotesParameters.MaximumPageSize);
    }

    [Fact]
    public void GivenCreateQuoteRequest_WhenMappingToHandlerRequest_MapsFieldsCorrectly()
    {
        // GIVEN
        CreateQuoteRequest request = new() 
        { 
            Value = "VALUE-FOR-TEST" 
        };

        // WHEN
        var result = Sut.Map<CreateQuoteRequest, CreateQuoteHandlerRequest>(request);

        // THEN 
        result.Should().NotBeNull();
        result.Value.Should().Be(request.Value);
    }

    [Fact]
    public void GivenCreateQuoteHandlerRequest_WhenMappingToModel_MapsFieldsCorrectlyAndFillsInPublicId()
    {
        // GIVEN
        CreateQuoteHandlerRequest request = new()
        {
            Value = "VALUE-FOR-TEST"
        };

        // WHEN
        var result = Sut.Map<CreateQuoteHandlerRequest, Quote>(request);

        // THEN 
        result.Should().NotBeNull();
        result.Value.Should().Be(request.Value);
        result.PublicId.Should().NotBe(Guid.Empty);
    }

    #endregion

    #region Response Mapping

    [Fact]
    public void GivenQuoteModel_WhenMappingToHandlerResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        Quote model = new()
        {
            PublicId = Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<Quote, QuoteResponseHandler>(model);

        // THEN 
        result.Should().NotBeNull();
        result.PublicId.Should().Be(model.PublicId);
        result.Created.Should().Be(model.Created);
        result.Updated.Should().Be(model.Updated);
        result.Value.Should().Be(model.Value);
    }

    [Fact]
    public void GivenQuoteResponseHandler_WhenMappingToContractResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        QuoteResponseHandler response = new()
        {
            PublicId = Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<QuoteResponseHandler, QuoteResponseContract>(response);

        // THEN 
        result.Should().NotBeNull();
        result.PublicId.Should().Be(response.PublicId);
        result.Created.Should().Be(response.Created);
        result.Updated.Should().Be(response.Updated);
        result.Value.Should().Be(response.Value);
    }

    [Fact]
    public void GivenGetQuoteHandlerResponse_WhenMappingToGetQuotesResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        GetQuotesHandlerResponse response = new()
        {
            Quotes = [
                new()
                {
                    PublicId = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Value = "VALUE-FOR-TEST"
                },
                new()
                {
                    PublicId = Guid.NewGuid(),
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    Value = "VALUE-FOR-TEST"
                }
            ]
        };

        // WHEN 
        var result = Sut.Map<GetQuotesHandlerResponse, GetQuotesResponse>(response);

        // THEN 
        result.Should().NotBeNull();
        result.Quotes.Should().NotBeNullOrEmpty();
        result.Quotes.Count().Should().Be(2);
    }

    [Fact]
    public void GivenGetQuoteByIdHandlerResponse_WhenMappingToGetQuoteByIdResponse_MapsFieldsCorrectly()
    {
        // GIVEN 

        GetQuoteByIdHandlerResponse response = new()
        {
            Quote = new()  
            {
                PublicId= Guid.NewGuid(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Value = "VALUE-FOR-TEST"
            }
        };

        // WHEN 
        var result = Sut.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(response);

        // THEN 
        result.Should().NotBeNull();
        result.Quote.Should().NotBeNull();
        result.Quote.PublicId.Should().Be(response.Quote.PublicId);
        result.Quote.Created.Should().Be(response.Quote.Created);
        result.Quote.Updated.Should().Be(response.Quote.Updated);
        result.Quote.Value.Should().Be(response.Quote.Value);
    }

    [Fact]
    public void GivenQuoteModel_WhenMappingToCreateQuoteHandlerResponse_MapsFieldsCorrectly()
    {
        // GIVEN 
        Quote model = new()
        {
            PublicId = Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<Quote, CreateQuoteHandlerResponse>(model);

        // THEN 
        result.Should().NotBeNull();
        result.PublicId.Should().Be(model.PublicId);
        result.Created.Should().Be(model.Created);
        result.Updated.Should().Be(model.Updated);
        result.Value.Should().Be(model.Value);
    }

    [Fact]
    public void GivenCreateQuoteHandlerResponse_WhenMappingToQuoteResponseContract_MapsFieldsCorrectly()
    {
        // GIVEN 
        CreateQuoteHandlerResponse model = new()
        {
            PublicId = Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<CreateQuoteHandlerResponse, QuoteResponseContract>(model);

        // THEN 
        result.Should().NotBeNull();
        result.PublicId.Should().Be(model.PublicId);
        result.Created.Should().Be(model.Created);
        result.Updated.Should().Be(model.Updated);
        result.Value.Should().Be(model.Value);
    }

    #endregion
}
