using Bogsi.Quotable.Application;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Builders.Models;

using Quote = Bogsi.Quotable.Application.Models.Quote;

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
        // GIVEN
        QuoteEntity entity = new QuoteEntityBuilder().Build();

        // WHEN 
        var result = Sut.Map<QuoteEntity, Quote>(entity);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(entity.PublicId, "PublicId should match entity");
        result.Value.Should().Be(entity.Value, "Value should match entity");
    }

    [Fact]
    public void GivenQuoteModel_WhenMappingToQuoteEntityForUpdate_MapsFieldsCorrectlyAndRetainsEntityDateValues()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();
        QuoteEntity entity = new QuoteEntityBuilder().WithPublicId(publicId).Build();
        Quote model = new QuoteBuilder().WithPublicId(publicId).WithValue("UPDATED-VALUE").Build();

        // WHEN
        var result = Sut.Map(model, entity);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        result.Created.Should().Be(entity.Created, "Created should match entity");
        result.Updated.Should().Be(entity.Updated, "Updated should match entity");
        result.Value.Should().Be(model.Value, "Value should match model");
    }

    #endregion

    #region Request Mapping

    [Fact]
    public void GivenGetQuotesParameters_WhenMappingToGetQuotesHandlerRequest_MapsFieldsCorrectly()
    {
        // GIVEN
        GetQuotesParameters parameters = new()
        {
            Cursor = 1,
            Size = 10,
            Origin = "ORIGIN",
            Tag = "TAG",
            SearchQuery = "SEARCHQUERY"
        };

        // WHEN 
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.Cursor.Should().Be(parameters.Cursor, "PageNumer should match parameters");
        result.Size.Should().Be(parameters.Size, "PageSize should match parameters");
        result.Origin.Should().Be(parameters.Origin, "Origin should match parameters");
        result.Tag.Should().Be(parameters.Tag, "Tag should match parameters");
        result.SearchQuery.Should().Be(parameters.SearchQuery, "SearchQuery should match parameters");
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(0, 0)]
    public void GivenGetQuotesParameters_WhenPageNumberOrPageSizeIsNullOrZero_ThenDefaultValuesAreProvided(int? cursor, int? size)
    {
        // GIVEN
        GetQuotesParameters parameters = new()
        {
            Cursor = cursor,
            Size = size
        };

        // WHEN
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THAN 
        result.Should().NotBeNull("Result should not be NULL");
        result.Cursor.Should().Be(Cursor.Default, "PageNumber should match constant");
        result.Size.Should().Be(Size.Default, "PageSize should match constant");
    }

    [Fact]
    public void GivenGetQuotesParameters_WhenPageSizeIsMoreThanMaximum_ThenMaximumValueIsProvided()
    {
        // GIVEN
        int wrongPageSize = Size.Maximum + 1;

        GetQuotesParameters parameters = new()
        {
            Size = wrongPageSize
        };

        // WHEN
        var result = Sut.Map<GetQuotesParameters, GetQuotesHandlerRequest>(parameters);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.Size.Should().Be(Size.Maximum, "PageSize should match constant");
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
        result.Should().NotBeNull("Result should not be NULL");
        result.Value.Should().Be(request.Value, "Value should match request");
    }

    [Fact]
    public void GivenCreateQuoteHandlerRequest_WhenMappingToQuoteModel_MapsFieldsCorrectlyAndFillsInPublicId()
    {
        // GIVEN
        CreateQuoteHandlerRequest request = new()
        {
            Value = "VALUE-FOR-TEST"
        };

        // WHEN
        var result = Sut.Map<CreateQuoteHandlerRequest, Quote>(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().NotBe(Guid.Empty, "PublicId should not be NULL");
        result.Value.Should().Be(request.Value, "Value should match request");
    }

    [Fact]
    public void GivenUpdateQuoteRequest_WhenMappingToUpdateQuoteHandlerRequest_MapsFieldsCorrectlyAndCopiesPublicId()
    {
        // GIVEN
        Guid publicId = Guid.NewGuid();

        UpdateQuoteRequest request = new()
        {
            Value = "VALUE-FOR-TEST"
        };

        // WHEN
        var result = Sut.Map<UpdateQuoteRequest, UpdateQuoteHandlerRequest>(request, opt => opt.Items["Id"] = publicId);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(publicId, "PublicId should match request");
        result.Value.Should().Be(request.Value, "Value should match request");
    }

    [Fact]
    public void GivenUpdateQuoteHandlerRequest_WhenMappingToModel_MapsFieldsCorrectly()
    {
        // GIVEN
        UpdateQuoteHandlerRequest request = new()
        {
            PublicId = Guid.NewGuid(),
            Value = "VALUE-FOR-TEST"
        };

        // WHEN
        var result = Sut.Map<UpdateQuoteHandlerRequest, Quote>(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(request.PublicId, "PublicId should match request");
        result.Value.Should().Be(request.Value, "Value should match request");
    }

    #endregion

    #region Response Mapping

    [Fact]
    public void GivenQuoteModel_WhenMappingToGetQuotesSingleQuoteHandlerResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        Quote model = new QuoteBuilder().Build();

        // WHEN 
        var result = Sut.Map<Quote, GetQuotesSingleQuoteHandlerResponse>(model);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        result.Value.Should().Be(model.Value, "Value should match model");
    }

    [Fact]
    public void GivenGetQuotesSingleQuoteHandlerResponse_WhenMappingToGetQuotesSingleQuoteResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        GetQuotesSingleQuoteHandlerResponse response = new()
        {
            PublicId = Guid.NewGuid(),
            Value = "VALUE-FOR-TEST"
        };

        // WHEN
        var result = Sut.Map<GetQuotesSingleQuoteHandlerResponse, GetQuotesSingleQuoteResponse>(response);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(response.PublicId, "PublicId should match response"); 
        result.Value.Should().Be(response.Value, "Value should match response");
    }

    [Fact]
    public void GivenGetQuotesHandlerResponse_WhenMappingToGetQuotesResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        List<GetQuotesSingleQuoteHandlerResponse> data =
        [
            new()
            {
                PublicId = Guid.NewGuid(),
                Value = "VALUE-FOR-TEST"
            },
            new()
            {
                PublicId = Guid.NewGuid(),
                Value = "VALUE-FOR-TEST"
            }
        ];

        GetQuotesHandlerResponse response = new()
        {
            Cursor = Cursor.Default + Cursor.Offset,
            Size = Size.Default,
            Total = data.Count,
            Data = data
        };

        // WHEN 
        var result = Sut.Map<GetQuotesHandlerResponse, GetQuotesResponse>(response);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.Data.Should().NotBeNullOrEmpty("Result should not be empty");
        result.Data.Count().Should().Be(2, "Result should contain 2 items");
        result.Cursor.Should().Be(Cursor.Default + Cursor.Offset, $"Cursor should be {Cursor.Default + Cursor.Offset}");
        result.Size.Should().Be(Size.Default, $"Size should be {Size.Default}");
        result.HasNext.Should().BeFalse("HasNext should be false");
    }


    [Fact]
    public void GivenQuoteModel_WhenMappingToGetQuoteByIdHandlerResponse_MapsFieldsCorrectly()
    {
        // GIVEN
        Quote model = new QuoteBuilder().Build();

        // WHEN 
        var result = Sut.Map<Quote, GetQuoteByIdHandlerResponse>(model);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        result.Created.Should().Be(model.Created, "Created should match model");
        result.Updated.Should().Be(model.Updated, "Updated should match model");
        result.Value.Should().Be(model.Value, "Value should match model");
    }

    [Fact]
    public void GivenGetQuoteByIdHandlerResponse_WhenMappingToGetQuoteByIdResponse_MapsFieldsCorrectly()
    {
        // GIVEN 

        GetQuoteByIdHandlerResponse response = new()
        {
            PublicId= Guid.NewGuid(),
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Value = "VALUE-FOR-TEST"
        };

        // WHEN 
        var result = Sut.Map<GetQuoteByIdHandlerResponse, GetQuoteByIdResponse>(response);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(response.PublicId, "PublicId should match response");
        result.Created.Should().Be(response.Created, "Created should match model");
        result.Updated.Should().Be(response.Updated, "Updated should match model");
        result.Value.Should().Be(response.Value, "Value should match response");
    }

    [Fact]
    public void GivenQuoteModel_WhenMappingToCreateQuoteHandlerResponse_MapsFieldsCorrectly()
    {
        // GIVEN 
        Quote model = new QuoteBuilder().Build();

        // WHEN 
        var result = Sut.Map<Quote, CreateQuoteHandlerResponse>(model);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        result.Created.Should().Be(model.Created, "Created should match model");
        result.Updated.Should().Be(model.Updated, "Updated should match model");
        result.Value.Should().Be(model.Value, "Value should match model");
    }

    [Fact]
    public void GivenCreateQuoteHandlerResponse_WhenMappingToCreateQuoteResponse_MapsFieldsCorrectly()
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
        var result = Sut.Map<CreateQuoteHandlerResponse, CreateQuoteResponse>(model);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        result.Created.Should().Be(model.Created, "Created should match model");
        result.Updated.Should().Be(model.Updated, "Updated should match model");
        result.Value.Should().Be(model.Value, "Value should match model");
    }

    #endregion
}
