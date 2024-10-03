using Bogsi.Quotable.Application.Errors;
using Bogsi.Quotable.Application.Interfaces.Utilities;
using Bogsi.Quotable.Test.Builders.Requests;

using Quote = Bogsi.Quotable.Application.Models.Quote;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

public class CreateQuoteHandlerTests : TestBase<CreateQuoteHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;
    private IUnitOfWork _unitOfWork = null!;
    private IMapper _mapper = null!;
    private CancellationToken _cancellationToken;

    protected override CreateQuoteHandler Construct()
    {
        _mapper = ConfigureMapper();
        _repository = Substitute.For<IRepository<Quote>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _cancellationToken = new();

        CreateQuoteHandler sut = new(
            _repository,
            _mapper,
            _unitOfWork);

        return sut;
    }

    #endregion

    [Fact]
    public async Task GivenCreateQuoteHandler_WhenValidRequestIsProvided_ThenNewQuoteIsCreatedAndReturned()
    {
        // GIVEN
        CreateQuoteHandlerRequest request = new CreateQuoteHandlerRequestBuilder().Build();

        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        // WHEN
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsSuccess.Should().BeTrue("Result should be success");
        result.IsFailure.Should().BeFalse("Result should be success");
        result.Value.Should().NotBeNull("Result should contain success value");
        result.Value.Value.Should().Be(request.Value, "Value should match the request");
        result.Value.PublicId.Should().NotBe(Guid.Empty, "PublicId should not be Guid.Empty");
    }

    [Fact]
    public async Task GivenCreateQuoteHandler_WhenSavingContextFails_ThenReturnQuotableError()
    {
        // GIVEN
        CreateQuoteHandlerRequest request = new CreateQuoteHandlerRequestBuilder().Build();

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
