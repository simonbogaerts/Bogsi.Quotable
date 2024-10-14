namespace Bogsi.Quotable.Test.Unit.Validators;

using Bogsi.Quotable.Application;
using Bogsi.Quotable.Application.Validators;
using Bogsi.Quotable.Test.Builders.Requests;
using Bogsi.Quotable.Test.Utilities;

using FluentValidation;


public sealed class CreateQuoteHandlerRequestValidatorTests : TestBase<IValidator<CreateQuoteHandlerRequest>>
{
    #region Test Setup

    protected override IValidator<CreateQuoteHandlerRequest> Construct()
    {
        CreateQuoteHandlerRequestValidator sut = new();

        return sut;
    }

    #endregion


    [Fact]
    public void GivenCreateQuoteHandlerRequest_WhenAllConditionsAreMet_ThenReturnIsValidTrue()
    {
        // GIVEN
        var request = new CreateQuoteHandlerRequestBuilder().Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeTrue("IsValid should be true");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("             ")]
    public void GivenCreateQuoteHandlerRequest_WhenValueIsNullOrEmpty_ThenReturnIsValidFalse(string? value)
    {
        // GIVEN
        var request = new CreateQuoteHandlerRequestBuilder().WithValue(value!).Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeFalse("IsValid should be false");
    }

    [Fact]
    public void GivenCreateQuoteHandlerRequest_WhenValueIsLongerTenMaximum_ThenReturnIsValidFalse()
    {
        // GIVEN
        var toLong = Constants.Quote.Properties.Value.MaximumLength + 1;
        var toLongValue = RandomStringGenerator.GenerateRandomString(toLong);

        var request = new CreateQuoteHandlerRequestBuilder().WithValue(toLongValue).Build();

        // WHEN
        var result = Sut.Validate(request);

        // THEN 
        result.Should().NotBeNull("Result should not be NULL");
        result.IsValid.Should().BeFalse("IsValid should be false");
    }
}
