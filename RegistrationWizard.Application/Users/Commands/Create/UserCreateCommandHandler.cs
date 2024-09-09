using AutoMapper;
using MediatR;
using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.Domain;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.Application.Users.Commands.Create;

public class UserCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UserCreateCommand, int>
{
    public async Task<int> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<UserModel>(request);

        model.Password = PasswordEncryptionService.Encrypt(request.Password);

        await unitOfWork.UserRepository.InsertAsync(model);
        await unitOfWork.SaveAsync();

        return model.Id;
    }
}