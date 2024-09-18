using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Utilities;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

public class UpdateQuoteHandlerTests : TestBase<IUpdateQuoteHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;
    private IMapper _mapper = null!;
    private IUnitOfWork _unitOfWork = null!;

    protected override IUpdateQuoteHandler Construct()
    {
        _mapper = ConfigureMapper();
        _repository = Substitute.For<IRepository<Quote>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        UpdateQuoteHandler sut = new(_repository, _mapper, _unitOfWork);

        return sut;
    }

    #endregion

    // No tests yet, as for now it would only test substitudes.
}
