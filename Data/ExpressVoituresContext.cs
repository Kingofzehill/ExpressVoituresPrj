using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models;
using Microsoft.Extensions.Hosting;

namespace ExpressVoitures.Data
{
    public class ExpressVoituresContext : DbContext
    {
        public ExpressVoituresContext (DbContextOptions<ExpressVoituresContext> options)
            : base(options)
        {
        }

        // FIX05 : define composite key :
        //      https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-8.0#composite-key.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReparationVehicule>().ToTable("ReparationVehicule");

            // composite key ReparationVehicule.
            modelBuilder.Entity<Vehicule>()
                .HasMany(e => e.Reparations)
                .WithMany(e => e.Vehicules)
                .UsingEntity<ReparationVehicule>();
            
            modelBuilder.Entity<Reparation>()
                .HasMany(e => e.Vehicules)
                .WithMany(e => e.Reparations)
                .UsingEntity<ReparationVehicule>();

            // FIX11 auto include for related data navigation.
            modelBuilder.Entity<Finition>().Navigation(e => e.Modele).AutoInclude();
            modelBuilder.Entity<Modele>().Navigation(e => e.Marque).AutoInclude();            
            modelBuilder.Entity<Vehicule>().Navigation(e => e.Finition).AutoInclude();
            modelBuilder.Entity<Vehicule>().Navigation(e => e.Image).AutoInclude();
            modelBuilder.Entity<Vehicule>().Navigation(e => e.Reparations).AutoInclude();
        }


        public DbSet<ExpressVoitures.Models.Vehicule> Vehicule { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Finition> Finition { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Marque> Marque { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Modele> Modele { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Reparation> Reparation { get; set; } = default!;
        //UPD06 add ReparationVehicule dbset (no dedicated controller // View) :
        //      https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-8.0#composite-key.
        public DbSet<ExpressVoitures.Models.ReparationVehicule> ReparationVehicule { get; set; } = default!;        
        public DbSet<Image> Images { get; set; }
    }
}
