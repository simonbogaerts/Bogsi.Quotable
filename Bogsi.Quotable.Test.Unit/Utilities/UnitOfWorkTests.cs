using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Infrastructure.Utilities;
using Bogsi.Quotable.Persistence;
using Bogsi.Quotable.Test.Builders.Entities;

namespace Bogsi.Quotable.Test.Unit.Utilities;

public sealed class UnitOfWorkTests : TestBase<IUnitOfWork>
{
    #region Test Setup

    private QuotableContext _quotable = null!;
    private CancellationToken _cancellationToken;

    protected override IUnitOfWork Construct()
    {
        _quotable = ConfigureDatabase();
        _cancellationToken = new CancellationToken();

        UnitOfWork sut = new(_quotable);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenUnitOfWork_WhenItemIsAddedToContext_ThenSaveChangesAsyncReturnsTrue()
    {
        // GIVEN
        QuoteEntity model = new QuoteEntityBuilder().Build();

        _quotable.Quotes.Add(model);

        // WHEN 
        var result = await Sut.SaveChangesAsync(_cancellationToken);

        // THEN 
        result.Should().BeTrue("Result should be true when an entity is saved");

        var newlyAddedQuote = _quotable.Quotes.FirstOrDefault();
        newlyAddedQuote!.PublicId.Should().Be(model.PublicId, "PublicId should match model");
        newlyAddedQuote!.Value.Should().Be(model.Value, "Value should match model");
    }
}
