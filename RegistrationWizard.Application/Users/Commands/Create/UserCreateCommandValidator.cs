using FluentValidation;
using RegistrationWizard.DataAccess.Abstractions;

namespace RegistrationWizard.Application.Users.Commands.Create;

public class UserCreateCommandValidator: AbstractValidator<UserCreateCommand>
{
    public const string EmptyLoginErrorMessage = "Login address is required";
    public const string LoginNotEmailErrorMessage = "A valid email is required";
    public const string EmptyPasswordErrorMessage = "Password is required";
    public const string EmptyCountryErrorMessage = "Country is required";
    public const string EmptyProvinceErrorMessage = "Province is required";
    public const string NotAgreeErrorMessage = "Agree is required";
    public const string UserAlreadyExistsErrorMessage = "The user already exists";

    public UserCreateCommandValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage(EmptyLoginErrorMessage)
            .EmailAddress().WithMessage(LoginNotEmailErrorMessage);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(EmptyPasswordErrorMessage);

        RuleFor(x => x.CountryId)
            .Must(x => x > 0)
            .WithMessage(EmptyCountryErrorMessage);

        RuleFor(x => x.ProvinceId)
            .Must(x => x > 0)
            .WithMessage(EmptyProvinceErrorMessage);

        RuleFor(x => x.IsAgree)
            .Must(x => x)
            .WithMessage(NotAgreeErrorMessage);

        RuleFor(x => x.Login)
            .MustAsync(async (id, _) =>
            {
                var exists = await unitOfWork.UserRepository.CheckExistsUserByEmail(id);

                return !exists;
            }).WithMessage(UserAlreadyExistsErrorMessage);
    }
}