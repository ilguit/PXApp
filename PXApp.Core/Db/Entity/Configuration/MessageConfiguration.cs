using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PXApp.Core.Db.Entity.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<TableMessage>
{
    public void Configure(EntityTypeBuilder<TableMessage> builder)
    {
        builder.ToTable("messages");
        builder.AddId();

        builder.Property(x => x.Body)
            .IsRequired();

        builder.AddDateCreated();
    }
}