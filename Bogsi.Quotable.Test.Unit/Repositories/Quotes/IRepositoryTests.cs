using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Infrastructure.Repositories;
using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Builders.Models;
using Quote = Bogsi.Quotable.Application.Models.Quote;

namespace Bogsi.Quotable.Test.Unit.Repositories.Quotes;

public class IRepositoryTests : TestBase<IRepository<Quote>>
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

    #region UpdateAsync

    [Fact]
    public async Task GivenUpdateAsync_WhenEntityIsNotFound_ThenReturnQuotableError()
    {
        // GIVEN
        Quote model = new QuoteBuilder().Build();

        // WHEN
        var result = await Sut.UpdateAsync(model, _cancellationToken);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.NotFound, "Error should be NotFound");
    }

    #endregion

    #region DeleteAsync

    [Fact]
    public async Task GivenDeleteAsync_WhenEntityIsNotFound_ThenReturnQuotableError()
    {
        // GIVEN
        Quote model = new QuoteBuilder().Build();

        // WHEN
        var result = await Sut.DeleteAsync(model, _cancellationToken);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.NotFound, "Error should be NotFound");
    }

    #endregion
}
