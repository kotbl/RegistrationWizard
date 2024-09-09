using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RegistrationWizard.DataAccess;

namespace RegistrationWizard.Application;

public class InitializationDataBase
{
    /// <summary>
    /// Initializing the database and applying migrations
    /// </summary>
    /// <param name="host"></param>
    public static void Update(IHost host)
    {
        using var scope = host.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<InitializationDataBase>>();

        logger.LogInformation("Start update databases");

        dbContext.Database.Migrate();
        dbContext.SaveChanges();

        logger.LogInformation("End update databases");
    }
}