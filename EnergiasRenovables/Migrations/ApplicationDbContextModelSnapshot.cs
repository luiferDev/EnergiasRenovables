﻿// <auto-generated />
using System;
using EnergiasRenovables.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EnergiasRenovables.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.Biomasa", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ContenidoEnergetico")
                        .HasColumnType("numeric");

                    b.Property<string>("MetodoConversion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Origen")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Biomasa");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaEolica", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("AlturaTurbinas")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DiametroTurbina")
                        .HasColumnType("numeric");

                    b.Property<int>("NumeroTurbinas")
                        .HasColumnType("integer");

                    b.Property<decimal>("VelocidadViento")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("EnergiaEolicas");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaGeotermica", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Caudal")
                        .HasColumnType("numeric");

                    b.Property<int>("NumeroPozos")
                        .HasColumnType("integer");

                    b.Property<decimal>("TemperaturaFluidos")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("EnergiaGeotermicas");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaHidroelectrica", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Caudal")
                        .HasColumnType("numeric");

                    b.Property<int>("NumeroTurbinas")
                        .HasColumnType("integer");

                    b.Property<decimal>("Salto")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("EnergiaHidroelectricas");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaRenovable", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TipoEnergiaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TipoEnergiaId");

                    b.ToTable("EnergiasRenovables");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaSolar", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("AnguloInclinacion")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AreaPaneles")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EficienciaPaneles")
                        .HasColumnType("numeric");

                    b.Property<decimal>("RadiacionSolar")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("EnergiaSolars");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.Pais", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<decimal>("EnergiaRequerida")
                        .HasColumnType("numeric");

                    b.Property<decimal>("NivelCovertura")
                        .HasColumnType("numeric");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PlantaProduccionId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Poblacion")
                        .HasColumnType("numeric");

                    b.HasKey("id");

                    b.ToTable("Paises");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.PlantaProduccion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<decimal>("CapacidadInstalada")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Eficiencia")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("FechaCreacion")
                        .HasColumnType("date");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlantaProduccions");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.TipoEnergia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipoEnergias");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.Biomasa", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.EnergiaRenovable", "EnergiaRenovable")
                        .WithOne("Biomasa")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.Biomasa", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergiaRenovable");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaEolica", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.EnergiaRenovable", "EnergiaRenovable")
                        .WithOne("EnergiaEolica")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.EnergiaEolica", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergiaRenovable");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaGeotermica", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.EnergiaRenovable", "EnergiaRenovable")
                        .WithOne("EnergiaGeotermica")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.EnergiaGeotermica", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergiaRenovable");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaHidroelectrica", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.EnergiaRenovable", "EnergiaRenovable")
                        .WithOne("EnergiaHidroelectrica")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.EnergiaHidroelectrica", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergiaRenovable");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaRenovable", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.PlantaProduccion", "PlantaProduccion")
                        .WithOne("EnergiaRenovable")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.EnergiaRenovable", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnergiasRenovables.Model.Entities.TipoEnergia", "TipoEnergia")
                        .WithMany("EnergiasRenovables")
                        .HasForeignKey("TipoEnergiaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlantaProduccion");

                    b.Navigation("TipoEnergia");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaSolar", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.EnergiaRenovable", "EnergiaRenovable")
                        .WithOne("EnergiaSolar")
                        .HasForeignKey("EnergiasRenovables.Model.Entities.EnergiaSolar", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnergiaRenovable");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.PlantaProduccion", b =>
                {
                    b.HasOne("EnergiasRenovables.Model.Entities.Pais", "Pais")
                        .WithMany("PlantaProduccion")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.EnergiaRenovable", b =>
                {
                    b.Navigation("Biomasa")
                        .IsRequired();

                    b.Navigation("EnergiaEolica")
                        .IsRequired();

                    b.Navigation("EnergiaGeotermica")
                        .IsRequired();

                    b.Navigation("EnergiaHidroelectrica")
                        .IsRequired();

                    b.Navigation("EnergiaSolar")
                        .IsRequired();
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.Pais", b =>
                {
                    b.Navigation("PlantaProduccion");
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.PlantaProduccion", b =>
                {
                    b.Navigation("EnergiaRenovable")
                        .IsRequired();
                });

            modelBuilder.Entity("EnergiasRenovables.Model.Entities.TipoEnergia", b =>
                {
                    b.Navigation("EnergiasRenovables");
                });
#pragma warning restore 612, 618
        }
    }
}
