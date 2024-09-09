using RegistrationWizard.Application.Countries.Queries.List;

namespace RegistrationWizard.Tests.Countries.Queries;

[TestFixture]
public class ListCountriesQueryHandlerTest
{
    [Test]
    public async Task HandleTest()
    {
        var countries = await Ioc.Mediator.Send(new ListCountryQuery());

        Assert.IsNotNull(countries);
        Assert.IsTrue(countries.Count > 0);
    }
}