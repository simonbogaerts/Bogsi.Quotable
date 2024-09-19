using Bogsi.Quotable.Application.Validators;
using Bogsi.Quotable.Test.Builders.Requests;
using Bogsi.Quotable.Test.Utilities;
using FluentValidation;

namespace Bogsi.Quotable.Test.Unit.Validators;

public class UpdateQuoteHandlerRequestValidatorTests : TestBase<IValidator<UpdateQuoteHandlerRequest>>
{
    #region Test Setup

    protected override UpdateQuoteHandlerRequestValidator Construct()
    {
        UpdateQuoteHandlerRequestValidator sut = new ();

        return sut;
    }

    #endregion

    [Fact]
    public void GivenUpdateQuoteHandlerRequest_WhenAllConditionsAreMet_ThenReturnIsValidTrue()
    {
        // GIVEN
        var request = new UpdateQuoteHandlerRequestBuilder().Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeTrue("IsValid should be true");
    }

    [Fact]
    public void GivenUpdateQuoteHandlerRequest_WhenGuidIsEmpty_ThenReturnIsValidFalse()
    {
        // GIVEN
        var request = new UpdateQuoteHandlerRequestBuilder().WithPublicId(Guid.Empty).Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeFalse("IsValid should be false");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("             ")]
    public void GivenUpdateQuoteHandlerRequest_WhenValueIsNullOrEmpty_ThenReturnIsValidFalse(string? value)
    {
        // GIVEN
        var request = new UpdateQuoteHandlerRequestBuilder().WithValue(value!).Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeFalse("IsValid should be false");
    }

    [Fact]
    public void GivenUpdateQuoteHandlerRequest_WhenValueIsLongerTenMaximum_ThenReturnIsValidFalse()
    {
        // GIVEN
        var toLong = QuoteProperties.Value.MaximumLength + 1;
        var toLongValue = RandomStringGenerator.GenerateRandomString(toLong);

        var request = new UpdateQuoteHandlerRequestBuilder().WithValue(toLongValue).Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeFalse("IsValid should be false");
    }
}
