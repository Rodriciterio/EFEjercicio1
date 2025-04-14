using EFEjercicio1Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFEjercicio1Data
{
    public class ConfectioneryContext : DbContext
    {
        public DbSet<Confectionery>Confectioneries { get; set; }
        public DbSet<Drink> Drinks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= RODRIGO\\SQL2022; Initial Catalog=LibraryDb; Trusted_Connection=true; TrustServerCertificate=true;")
                .UseLazyLoadingProxies(false);
                //.EnableSensitiveDataLogging() // Permite ver valores en las consultas
                // .LogTo(Console.WriteLine, LogLevel.Information)
                // 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("Drinks");
                entity.HasKey(d=> d.Id);
                entity.Property(d => d.Name).HasMaxLength(100)
                .IsRequired();
                entity.Property(d => d.Size).HasMaxLength(50)
                .IsRequired();
                entity.HasIndex(d => new { d.Name, d.Size }, "IX_Drinks_DrinkId").IsUnique();
                entity.HasOne(d => d.Confectionery).WithMany(d => d.Drinks).HasForeignKey(d => d.ConfectioneryId)
                .OnDelete(DeleteBehavior.ClientNoAction);

                var drinkList = new List<Drink>
                {
                    new Drink{ Id = 8, Name="Americano", Size="Mediano", ConfectioneryId=5},
                     new Drink{ Id = 9, Name="Irish Coffe", Size="Mediano", ConfectioneryId=2}
                };
                entity.HasData(drinkList);
            });
        }
    }
}
