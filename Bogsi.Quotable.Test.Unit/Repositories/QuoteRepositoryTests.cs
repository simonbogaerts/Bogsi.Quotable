using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Builders.Entities;

namespace Bogsi.Quotable.Test.Unit.Repositories;

public class QuoteRepositoryTests : TestBase<IRepository<Quote>>
{
    #region Test Setup

    private QuotableContext _quotable = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override IRepository<Quote> Construct()
    {
        _quotable = ConfigureDatabase();
        _mapper = ConfigureMapper();
        _cancellationToken = new CancellationToken();

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

        // WHEN
        var result = await Sut.GetAsync(_cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.Count.Should().Be(2, "Result should contain 2 items");
    }

    [Fact]
    public async Task GivenGetAsync_WhenNoQuotesInDatabase_ThenReturnsEmptyCollection()
    {
        // GIVEN
        // WHEN
        var result = await Sut.GetAsync(_cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.Count.Should().Be(0, "Result should contain 0 items");
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
        result!.PublicId.Should().Be(publicId, "PublicId should match entity");
        result!.Value.Should().Be(value, "Value should match entity");
    }

    [Fact]
    public async Task GivenGetByIdAsync_WhenPublicIdDoesNotMatchAny_ThenReturnNull()
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
        result.Should().BeNull("Result should be NULL");
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
        result.Should().BeTrue("Result should be true");
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
        result.Should().BeFalse("Result should be false");
    }

    #endregion
}
