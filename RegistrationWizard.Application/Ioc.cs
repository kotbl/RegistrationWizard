using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationWizard.Application.Countries.MappingConfig;
using RegistrationWizard.Application.PipelineBehaviors;
using RegistrationWizard.Application.Provinces.MappingConfig;
using RegistrationWizard.DataAccess;
using RegistrationWizard.DataAccess.Abstractions;
using RegistrationWizard.DataAccess.Repositories;

namespace RegistrationWizard.Application;

public static class Ioc
{
    /// <summary>
    /// Database connection string configuration property name
    /// </summary>
    public const string ConnectionString = "DefaultConnection";

    /// <summary>
    /// Add dependencies for DataAccess
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration</param>
    public static void DataAccessRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConnectionString)));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }

    public static void ApplicationRegistration(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Ioc).Assembly;
        services.AddValidatorsFromAssembly(applicationAssembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(applicationAssembly);
            cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddAutoMapper(typeof(CountryProfile), typeof(ProvinceProfile));
    }
}