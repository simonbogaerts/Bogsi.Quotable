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

    [Fact]
    public async Task GivenGetAsync_WhenParametersAreOfNoConcequence_ReturnAllQuotes()
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


    // TEST NULL FROM DB
}
