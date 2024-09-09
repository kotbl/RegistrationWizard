using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.ModelConfigurations;

internal class ProvinceConfiguration : BaseConfiguration<ProvinceModel>
{
    public override void Configure(EntityTypeBuilder<ProvinceModel> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CountryId).IsRequired();

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Province)
            .HasForeignKey(x => x.ProvinceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Country)
            .WithMany(x => x.Provinces)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
