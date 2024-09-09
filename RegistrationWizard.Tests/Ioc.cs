using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RegistrationWizard.Application;
using RegistrationWizard.DataAccess;
using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.DataAccess.Repositories;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.Tests;

public static class Ioc
{
    private const string TestDataBaseName = "TestDataBase";

    private static IServiceProvider? _serviceProvider;

    public static IMediator Mediator => ServiceProvider.GetRequiredService<IMediator>();

    public static IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider is not null)
                return _serviceProvider;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase(TestDataBaseName)
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
            serviceCollection.ApplicationRegistration();
            serviceCollection.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            _serviceProvider = serviceCollection.BuildServiceProvider();

            Task.WaitAll(FillDataBases(_serviceProvider));

            return _serviceProvider;
        }
    }

    private static async Task FillDataBases(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();

        var countries = new List<CountryModel>();
        var provinces = new List<ProvinceModel>();
        var provinceId = 1;
        var initItemCount = 4;

        for (var i = 0; i < initItemCount; i++)
        {
            var countryId = i + 1;

            countries.Add(new CountryModel
            {
                Id = countryId,
                Name = $"Country {countryId}",
                CreateDt = DateTimeOffset.UtcNow,
                UpdateDt = DateTimeOffset.UtcNow,
            });

            for (var j = 0; j < initItemCount; j++)
            {
                provinces.Add(new ProvinceModel()
                {
                    Id = provinceId++,
                    CountryId = countryId,
                    Name = $"Province {provinceId}",
                    CreateDt = DateTimeOffset.UtcNow,
                    UpdateDt = DateTimeOffset.UtcNow,
                });
            }
        }

        await unitOfWork.CountryRepository.InsertRangeAsync(countries);
        await unitOfWork.ProvinceRepository.InsertRangeAsync(provinces);

        await unitOfWork.SaveAsync();
    }
}