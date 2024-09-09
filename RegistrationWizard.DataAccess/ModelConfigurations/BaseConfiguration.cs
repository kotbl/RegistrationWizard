using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RegistrationWizard.DataAccess.ModelConfigurations;

internal class BaseConfiguration<TModel> : IEntityTypeConfiguration<TModel> where TModel : BaseModel
{
    protected static string SqlGetDateFunc => "SYSDATETIMEOFFSET()";

    public virtual void Configure(EntityTypeBuilder<TModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreateDt)
            .IsRequired()
            .HasDefaultValueSql(SqlGetDateFunc)
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);

        builder.Property(x => x.UpdateDt)
            .IsRequired()
            .HasDefaultValueSql(SqlGetDateFunc)
            .ValueGeneratedOnAddOrUpdate()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
    }
}
