using RegistrationWizard.Application.Countries.Queries.List;
using RegistrationWizard.Application.Provinces.List;

namespace RegistrationWizard.Tests.Provinces.List;

[TestFixture]
public class ListProvinceQueryHandlerTest
{
    [Test]
    public async Task HandleTest()
    {
        var mediator = Ioc.Mediator;

        var countries = await mediator.Send(new ListCountryQuery());
        var provinces = await mediator.Send(new ListProvinceQuery(){CountryId = countries.First().Id});

        Assert.IsNotNull(provinces);
        Assert.IsTrue(provinces.Count > 0);
    }
}