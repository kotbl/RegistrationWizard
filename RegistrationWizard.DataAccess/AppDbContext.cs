using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DataAccess.ModelConfigurations;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<CountryModel> Countries { get; set; }
    public DbSet<ProvinceModel> Provinces { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new ProvinceConfiguration());

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

        modelBuilder.Entity<CountryModel>().HasData(countries);
        modelBuilder.Entity<ProvinceModel>().HasData(provinces);
    }
}