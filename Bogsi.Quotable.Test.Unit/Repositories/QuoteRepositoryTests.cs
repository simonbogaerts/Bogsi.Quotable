using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;
using FluentAssertions;

namespace Bogsi.Quotable.Test.Unit.Repositories;

public class QuoteRepositoryTests : TestBaseWithContext<IRepository<Quote>>
{
    #region Test Setup

    private QuotableContext _quotable = null!;
    private CancellationToken _cancellationToken;

    protected override IRepository<Quote> Construct()
    {
        _quotable = SetupQuotableDatabase();
        _cancellationToken = new CancellationToken();

        QuoteRepository sut = new(_quotable);

        return sut;
    }

    #endregion

    [Fact]
    public void GivenGetAsync_WhenParametersAreOfNoConcequence_ReturnAllQuotes()
    {
        // GIVEN

        // WHEN
        var result = Sut.GetAsync(_cancellationToken);

        // THEN 
        result.Should().NotBeNull();
    }
}
