using MediatR;

namespace RegistrationWizard.Application.Users.Commands.Create;

public record UserCreateCommand(string Login, string Password, bool IsAgree, int CountryId, int ProvinceId): IRequest<int>;