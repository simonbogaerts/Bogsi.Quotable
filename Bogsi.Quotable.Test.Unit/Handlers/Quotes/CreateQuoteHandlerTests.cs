using Bogsi.Quotable.Application.Handlers.Quotes.CreateQuote;
using Bogsi.Quotable.Application.Interfaces.Utilities;

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
        _cancellationToken = new CancellationToken();

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
        CreateQuoteHandlerRequest request = new()
        {
            Value = "VALUE-FOR-TEST"
        };

        _unitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(true);

        // WHEN
        var result = await Sut.HandleAsync(request, _cancellationToken);

        // THEN 
        result.Should().NotBeNull();
        result.Value.Should().Be(request.Value);
        result.PublicId.Should().NotBe(Guid.Empty);
    }
}
