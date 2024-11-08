using EnergiasRenovables.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnergiasRenovables.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<EnergiaSolar> EnergiaSolars { get; set; }
        public virtual DbSet<EnergiaRenovable> EnergiasRenovables { get; set; }
        public virtual DbSet<PlantaProduccion> PlantaProduccions { get; set; }
        public virtual DbSet<TipoEnergia> TipoEnergias { get; set; }
        public virtual DbSet<Biomasa> Biomasa { get; set; }
        public virtual DbSet<EnergiaHidroelectrica> EnergiaHidroelectricas { get; set; }
        public virtual DbSet<EnergiaGeotermica> EnergiaGeotermicas { get; set; }
        public virtual DbSet<EnergiaEolica> EnergiaEolicas { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }

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
