using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.ModelConfigurations;

internal class CountryConfiguration : BaseConfiguration<CountryModel>
{
    public override void Configure(EntityTypeBuilder<CountryModel> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Provinces)
            .WithOne(x => x.Country)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
