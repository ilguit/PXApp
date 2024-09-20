using PXApp.Common.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PXApp.Core.Db.Entity.Configuration;

internal static class ConfigurationHelper
{
    internal static void AddId<T>(this EntityTypeBuilder<T> builder)
        where T : class, IHasId
    {
        builder.Property(x => x.Id)
            .HasDefaultValueSql(DbDefaultValues.GeneratedId)
            .IsRequired();
    }

    internal static void AddDateCreated<T>(this EntityTypeBuilder<T> builder)
        where T : class, IHasDateCreated
    {
        builder.Property(x => x.DateCreated)
            .HasColumnType(DbTypes.UtcDateTime)
            .HasDefaultValueSql(DbDefaultValues.CurrentUtcDateTime)
            .IsRequired();
    }
}