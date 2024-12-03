using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EnergiasRenovables.Data
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<EnergiaSolar> EnergiaSolars { get; set; }
        public required DbSet<EnergiaRenovable> EnergiasRenovables { get; set; }
        public required DbSet<PlantaProduccion> PlantaProduccions { get; set; }
        public required DbSet<TipoEnergia> TipoEnergias { get; set; }
        public required DbSet<Biomasa> Biomasa { get; set; }
        public required DbSet<EnergiaHidroelectrica> EnergiaHidroelectricas { get; set; }
        public required DbSet<EnergiaGeotermica> EnergiaGeotermicas { get; set; }
        public required DbSet<EnergiaEolica> EnergiaEolicas { get; set; }
        public required DbSet<Pais> Paises { get; set; }
        public required DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnergiaSolar>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<EnergiaEolica>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<EnergiaHidroelectrica>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Biomasa>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<EnergiaGeotermica>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<PlantaProduccion>()
                    .HasKey(e => e.Id);

            modelBuilder.Entity<EnergiaRenovable>()
                .HasOne(e => e.EnergiaSolar)
                .WithOne(d => d.EnergiaRenovable)
                .HasForeignKey<EnergiaSolar>(d => d.Id)
                .IsRequired();

            modelBuilder.Entity<EnergiaRenovable>()
                .HasOne(e => e.EnergiaEolica)
                .WithOne(d => d.EnergiaRenovable)
                .HasForeignKey<EnergiaEolica>(d => d.Id)
                .IsRequired();

            modelBuilder.Entity<EnergiaRenovable>()
                .HasOne(e => e.EnergiaHidroelectrica)
                .WithOne(d => d.EnergiaRenovable)
                .HasForeignKey<EnergiaHidroelectrica>(d => d.Id)
                .IsRequired();

            modelBuilder.Entity<EnergiaRenovable>()
                .HasOne(e => e.Biomasa)
                .WithOne(d => d.EnergiaRenovable)
                .HasForeignKey<Biomasa>(d => d.Id)
                .IsRequired();

            modelBuilder.Entity<EnergiaRenovable>()
                .HasOne(e => e.EnergiaGeotermica)
                .WithOne(d => d.EnergiaRenovable)
                .HasForeignKey<EnergiaGeotermica>(d => d.Id)
                .IsRequired();


            // Relación one-to-one entre PlantaProduccion y EnergiasRenovables
            modelBuilder.Entity<PlantaProduccion>()
                .HasOne(p => p.EnergiaRenovable)
                .WithOne(e => e.PlantaProduccion)
                .HasForeignKey<EnergiaRenovable>(e => e.Id);


            // Relación one-to-many entre TipoEnergia y EnergiasRenovables
            modelBuilder.Entity<TipoEnergia>()
                .HasMany(t => t.EnergiasRenovables)
                .WithOne(e => e.TipoEnergia)
                .HasForeignKey(e => e.TipoEnergiaId);

            modelBuilder.Entity<Pais>()
                .HasMany(p => p.PlantaProduccion)
                .WithOne(e => e.Pais)
                .HasForeignKey(e => e.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
