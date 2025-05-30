﻿// <auto-generated />
using EFEjercicio1Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    [DbContext(typeof(ConfectioneryContext))]
    [Migration("20250411025124_SetNoCascadeDeletion")]
    partial class SetNoCascadeDeletion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFEjercicio1Entities.Confectionery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_Confectionaries_Name")
                        .IsUnique();

                    b.ToTable("Confectioneries");
                });

            modelBuilder.Entity("EFEjercicio1Entities.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConfectioneryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ConfectioneryId");

                    b.HasIndex(new[] { "Name", "Size" }, "IX_Drinks_DrinkId")
                        .IsUnique();

                    b.ToTable("Drinks", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 8,
                            ConfectioneryId = 5,
                            Name = "Americano",
                            Size = "Mediano"
                        },
                        new
                        {
                            Id = 9,
                            ConfectioneryId = 2,
                            Name = "Irish Coffe",
                            Size = "Mediano"
                        });
                });

            modelBuilder.Entity("EFEjercicio1Entities.Drink", b =>
                {
                    b.HasOne("EFEjercicio1Entities.Confectionery", "Confectionery")
                        .WithMany("Drinks")
                        .HasForeignKey("ConfectioneryId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.Navigation("Confectionery");
                });

            modelBuilder.Entity("EFEjercicio1Entities.Confectionery", b =>
                {
                    b.Navigation("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}
