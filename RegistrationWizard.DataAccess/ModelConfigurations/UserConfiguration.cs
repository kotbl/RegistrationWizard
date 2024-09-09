using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Domain.Models;

namespace RegistrationWizard.DataAccess.ModelConfigurations;

internal class UserConfiguration : BaseConfiguration<UserModel>
{
    public override void Configure(EntityTypeBuilder<UserModel> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.CountryId).IsRequired();
        builder.Property(x => x.ProvinceId).IsRequired();

        builder.HasOne(x => x.Country).WithMany(x => x.Users).HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Province).WithMany(x => x.Users).HasForeignKey(x => x.ProvinceId).OnDelete(DeleteBehavior.NoAction);
    }
}
