using Bogsi.Quotable.Application.Handlers.Quotes.GetQuotes;
using Bogsi.Quotable.Application.Interfaces.Repositories;
using Bogsi.Quotable.Application.Models;
using NSubstitute;

namespace Bogsi.Quotable.Test.Unit.Handlers.Quotes.GetQuotes;

public class GetQuotesHandlerTests : TestBase<IGetQuotesHandler>
{
    #region Test Setup

    private IRepository<Quote> _repository = null!;

    protected override IGetQuotesHandler Construct()
    {
        _repository = Substitute.For<IRepository<Quote>>();

        GetQuotesHandler sut = new(_repository);

        return sut;
    }

    #endregion
}
