using EFEjercicio1Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFEjercicio1Data.EntityTipeConfigurations
{
    public class ConfectioneryEntityTypeConfiguration : IEntityTypeConfiguration<Confectionery>
    {
        public void Configure(EntityTypeBuilder<Confectionery> entity)
        {
            entity.ToTable("Confectioneries");

            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(c => new { c.Name }, "IX_Confectioneries_Name")
                .IsUnique();

            entity.HasMany(c => c.Drinks)
                .WithOne(d => d.Confectionery)
                .HasForeignKey(d => d.ConfectioneryId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
