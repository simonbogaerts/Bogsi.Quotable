using Bogsi.Quotable.Application.Handlers.Quotes;
using Bogsi.Quotable.Application.Interfaces.Utilities;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes;

public class DeleteQuoteHandlerTests : TestBase<IDeleteQuoteHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;
    private IUnitOfWork _unitOfWork = null!;

    protected override IDeleteQuoteHandler Construct()
    {
        _repository = Substitute.For<IRepository<Quote>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        DeleteQuoteHandler sut = new(_repository, _unitOfWork);

        return sut;
    }

    #endregion

    // No tests yet, as for now it would only test substitudes. 
}
