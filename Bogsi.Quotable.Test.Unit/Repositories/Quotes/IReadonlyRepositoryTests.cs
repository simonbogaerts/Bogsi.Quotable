using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Builders.Entities;
using Bogsi.Quotable.Test.Builders.Requests;

using Quote = Bogsi.Quotable.Application.Models.Quote;

namespace Bogsi.Quotable.Test.Unit.Repositories.Quotes;

public sealed class IReadonlyRepositoryTests : TestBase<IReadonlyRepository<Quote>>
{
    #region Test Setup

    private QuotableContext _quotable = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override IRepository<Quote> Construct()
    {
        _quotable = ConfigureDatabase();
        _mapper = ConfigureMapper();
        _cancellationToken = new();

        QuoteRepository sut = new(
            _quotable,
            _mapper);

        return sut;
    }

    #endregion

    #region GetAsync

    [Fact]
    public async Task GivenGetAsync_WhenParametersAreOfNoConcequence_ThenReturnAllQuotesAsBusinessModel()
    {
        // GIVEN
        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().Build(),
            new QuoteEntityBuilder().Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().Build();

        // WHEN
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should contain success value");
        result.Value.Data.Count.Should().Be(2, "Result should contain 2 items");
    }

    [Fact]
    public async Task GivenGetAsync_WhenNoQuotesInDatabase_ThenReturnsEmptyCollection()
    {
        // GIVEN
        GetQuotesHandlerRequest request = new GetQuotesHandlerRequestBuilder().Build();

        // WHEN
        var result = await Sut.GetAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Data.Should().NotBeNull("Result should have an initialized collection");
        result.Value.Data.Count.Should().Be(0, "Result should have an empty collection");
    }

    #endregion

    #region GetByIdAsync

    [Fact]
    public async Task GivenGetByIdAsync_WhenPublicIdMatches_ThenReturnQuoteAsBusinessModel()
    {
        // GIVEN
        var publicId = Guid.NewGuid();
        var value = "ENTITY-VALUE";

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithPublicId(publicId).WithValue(value).Build(),
            new QuoteEntityBuilder().Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.GetByIdAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should not be NULL");
        result.Value.PublicId.Should().Be(publicId, "PublicId should match entity");
        result.Value.Value.Should().Be(value, "Value should match entity");
    }

    [Fact]
    public async Task GivenGetByIdAsync_WhenPublicIdDoesNotMatchAny_ThenReturnQuotableError()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().Build(),
            new QuoteEntityBuilder().Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.GetByIdAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.NotFound, "Error should be NotFound");
    }

    #endregion

    #region ExistsAsync

    [Fact]
    public async Task GivenExistsAsync_WhenPublicIdMatchesExistingQuote_ThenReturnTrue()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().WithPublicId(publicId).Build(),
            new QuoteEntityBuilder().Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.ExistsAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().BeTrue("Result should be true");
    }

    [Fact]
    public async Task GivenExistsAsync_WhenPublicIdDoesNotMatchAny_ThenReturnFalse()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntityBuilder().Build(),
            new QuoteEntityBuilder().Build()
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.ExistsAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().BeFalse("Result should be false");
    }

    #endregion
}
