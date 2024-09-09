using AutoMapper;
using MediatR;
using RegistrationWizard.Application.Provinces.Dtos;
using RegistrationWizard.DataAccess.Abstractions;

namespace RegistrationWizard.Application.Provinces.List;

public class ListProvinceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<ListProvinceQuery, List<ProvinceDto>>
{
    public async Task<List<ProvinceDto>> Handle(ListProvinceQuery request, CancellationToken cancellationToken)
    {
        var models = await unitOfWork.ProvinceRepository.GetAsync(x => x.CountryId == request.CountryId);

        return mapper.Map<List<ProvinceDto>>(models);
    }
}
