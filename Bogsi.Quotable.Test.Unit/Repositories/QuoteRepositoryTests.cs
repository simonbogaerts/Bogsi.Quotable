using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;

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
            new QuoteEntity { Id = 1, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now },
            new QuoteEntity { Id = 2, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now }
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.GetAsync(_cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be null.");
        result.Count.Should().Be(2, "Result should have 2 items.");
    }

    [Fact]
    public async Task GivenGetAsync_WhenNoQuotesInDatabase_ThenReturnsEmptyCollection()
    {
        // GIVEN
        // WHEN
        var result = await Sut.GetAsync(_cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be null.");
        result.Count.Should().Be(0, "Result should have 0 items.");
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
            new QuoteEntity { Id = 1, PublicId = publicId, Value = value, Created = DateTime.Now, Updated = DateTime.Now },
            new QuoteEntity { Id = 2, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now }
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.GetByIdAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be null.");
        result!.PublicId.Should().Be(publicId);
        result!.Value.Should().Be(value);
    }

    [Fact]
    public async Task GivenGetByIdAsync_WhenPublicIdDoesNotMatchAny_ThenReturnNull()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntity { Id = 1, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now },
            new QuoteEntity { Id = 2, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now }
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.GetByIdAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().BeNull("Result should be null.");
    }

    #endregion

    #region ExistsAsync

    [Fact]
    public async Task GivenExistsAsync_WhenPublicIdMatchesExistingQuote_ThenReturnTrue()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntity { Id = 1, PublicId = publicId, Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now },
            new QuoteEntity { Id = 2, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now }
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.ExistsAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().BeTrue("Result should be true.");
    }

    [Fact]
    public async Task GivenExistsAsync_WhenPublicIdDoesNotMatchAny_ThenReturnFalse()
    {
        // GIVEN
        var publicId = Guid.NewGuid();

        List<QuoteEntity> quoteEntities = [
            new QuoteEntity { Id = 1, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now },
            new QuoteEntity { Id = 2, PublicId = Guid.NewGuid(), Value = "VALUE", Created = DateTime.Now, Updated = DateTime.Now }
        ];

        _quotable.Quotes.AddRange(quoteEntities);
        _quotable.SaveChanges();

        // WHEN
        var result = await Sut.ExistsAsync(publicId, _cancellationToken);

        // THEN 
        result.Should().BeFalse("Result should be false.");
    }

    #endregion
}
