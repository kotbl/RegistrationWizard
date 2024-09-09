using FluentValidation;
using MediatR;
using RegistrationWizard.Application.Users.Commands.Create;

namespace RegistrationWizard.Tests.Users.Commands;

[TestFixture]
public class UserCreateCommandHandlerTests
{
    private IMediator Mediator { get; set; }

    [SetUp]
    public void SetUp()
    {
        Mediator = Ioc.Mediator;
    }

    [Test]
    public async Task LoginEmptyValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand(string.Empty, "1234", true, 1, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.EmptyLoginErrorMessage));
        }
    }

    [Test]
    public async Task LoginEmailValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test", "1234", true, 1, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.LoginNotEmailErrorMessage));
        }
    }

    [Test]
    public async Task PasswordValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test@test.com", string.Empty, true, 1, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.EmptyPasswordErrorMessage));
        }
    }

    [Test]
    public async Task CountryValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test@test.com", "1234", true, 0, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.EmptyCountryErrorMessage));
        }
    }

    [Test]
    public async Task ProvinceValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test@test.com", "1234", true, 1, 0));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.EmptyProvinceErrorMessage));
        }
    }

    [Test]
    public async Task AgreeValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test@test.com", "1234", false, 1, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.NotAgreeErrorMessage));
        }
    }

    [Test]
    public async Task UserAlreadyExistsValidationTest()
    {
        try
        {
            await Mediator.Send(new UserCreateCommand("test@test.com", "1234", true, 1, 1));
            await Mediator.Send(new UserCreateCommand("test@test.com", "1234", true, 1, 1));

            Assert.Fail();
        }
        catch (Exception e)
        {
            var ex = e as ValidationException;

            Assert.IsNotNull(ex);
            Assert.IsTrue(ex.Errors.Select(x => x.ErrorMessage).Contains(UserCreateCommandValidator.UserAlreadyExistsErrorMessage));
        }
    }
}