using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Test.Builders.Requests;

using Quote = Bogsi.Quotable.Application.Models.Quote;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

public class UpdateQuoteHandlerTests : TestBase<IUpdateQuoteHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;
    private IMapper _mapper = null!;
    private IUnitOfWork _unitOfWork = null!;
    private CancellationToken _cancellationToken;

    protected override IUpdateQuoteHandler Construct()
    {
        _mapper = ConfigureMapper();
        _repository = Substitute.For<IRepository<Quote>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _cancellationToken = new();

        UpdateQuoteHandler sut = new(_repository, _mapper, _unitOfWork);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenUpdateQuoteHandler_WhenQuoteDoesNotExist_ThenNewQuotableErrorIsReturned()
    {
        // GIVEN
        UpdateQuoteHandlerRequest request = new UpdateQuoteHandlerRequestBuilder().Build();

        _repository.UpdateAsync(Arg.Any<Quote>(), Arg.Any<CancellationToken>()).Returns(QuotableErrors.NotFound);

        // WHEN 
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeFalse("Result should be failure");
        result.IsFailure.Should().BeTrue("Result should be failure");
        result.Error.Should().Be(QuotableErrors.NotFound, "Error should be NotFound");
    }

    [Fact]
    public async Task GivenUpdateQuoteHandler_WhenSavingContextFails_ThenNewQuotableErrorIsReturned()
    {
        // GIVEN
        UpdateQuoteHandlerRequest request = new UpdateQuoteHandlerRequestBuilder().Build();

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
