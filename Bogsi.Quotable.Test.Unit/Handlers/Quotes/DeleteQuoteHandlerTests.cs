using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Test.Builders.Requests;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

public class DeleteQuoteHandlerTests : TestBase<IDeleteQuoteHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private CancellationToken _cancellationToken;

    protected override IDeleteQuoteHandler Construct()
    {
        _repository = Substitute.For<IRepository<Quote>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _cancellationToken = new();

        DeleteQuoteHandler sut = new(_repository, _unitOfWork);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenDeleteQuoteHandler_WhenQuoteDoesNotExist_ThenNewQuotableErrorIsReturned()
    {
        // GIVEN
        DeleteQuoteHandlerRequest request = new DeleteQuoteHandlerRequestBuilder().Build();

        _repository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(QuotableErrors.NotFound);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.NotFound, "Error should be NotFound");
    }

    [Fact]
    public async Task GivenDeleteQuoteHandler_WhenSavingContextFails_ThenNewQuotableErrorIsReturned()
    {
        // GIVEN
        DeleteQuoteHandlerRequest request = new DeleteQuoteHandlerRequestBuilder().Build();

        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(false);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.InternalError, "Error should be InternalError");
    }
}
