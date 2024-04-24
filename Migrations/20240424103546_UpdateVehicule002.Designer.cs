﻿// <auto-generated />
using System;
using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpressVoitures.Migrations
{
    [DbContext(typeof(ExpressVoituresContext))]
    [Migration("20240424103546_UpdateVehicule002")]
    partial class UpdateVehicule002
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpressVoitures.Models.Finition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LibelleFinition")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ModeleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModeleId");

                    b.ToTable("Finition");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Marque", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LibelleMarque")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Marque");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Modele", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LibelleModele")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MarqueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarqueId");

                    b.ToTable("Modele");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Reparation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LibelleReparation")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Reparation");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Vehicule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnneeVehicule")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAchat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateMisAJour")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateMisEnVente")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateVente")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinitionId")
                        .HasColumnType("int");

                    b.Property<string>("Information")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Marge")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<bool>("MisEnVente")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Photo")
                        .HasMaxLength(4000)
                        .HasColumnType("varbinary(4000)");

                    b.Property<decimal>("PrixAchat")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<decimal?>("PrixDeVente")
                        .HasPrecision(7, 2)
                        .HasColumnType("decimal(7,2)");

                    b.Property<int>("Statut")
                        .HasColumnType("int");

                    b.Property<bool>("Vendu")
                        .HasColumnType("bit");

                    b.Property<string>("Vin")
                        .IsRequired()
                        .HasMaxLength(17)
                        .HasColumnType("nvarchar(17)");

                    b.HasKey("Id");

                    b.HasIndex("FinitionId");

                    b.ToTable("Vehicule");
                });

            modelBuilder.Entity("ReparationVehicule", b =>
                {
                    b.Property<int>("ReparationsId")
                        .HasColumnType("int");

                    b.Property<int>("VehiculesId")
                        .HasColumnType("int");

                    b.HasKey("ReparationsId", "VehiculesId");

                    b.HasIndex("VehiculesId");

                    b.ToTable("ReparationVehicule");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Finition", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Modele", "Modele")
                        .WithMany("Finitions")
                        .HasForeignKey("ModeleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modele");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Modele", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Marque", "Marque")
                        .WithMany("Modeles")
                        .HasForeignKey("MarqueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Marque");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Vehicule", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Finition", "Finition")
                        .WithMany("Vehicules")
                        .HasForeignKey("FinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Finition");
                });

            modelBuilder.Entity("ReparationVehicule", b =>
                {
                    b.HasOne("ExpressVoitures.Models.Reparation", null)
                        .WithMany()
                        .HasForeignKey("ReparationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExpressVoitures.Models.Vehicule", null)
                        .WithMany()
                        .HasForeignKey("VehiculesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExpressVoitures.Models.Finition", b =>
                {
                    b.Navigation("Vehicules");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Marque", b =>
                {
                    b.Navigation("Modeles");
                });

            modelBuilder.Entity("ExpressVoitures.Models.Modele", b =>
                {
                    b.Navigation("Finitions");
                });
#pragma warning restore 612, 618
        }
    }
}
