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

        // FIX05 : define composite key, exception : requires a primary key to be defined.
        //  https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-8.0#composite-key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReparationVehicule>().ToTable("ReparationVehicule");

            //Define composite key
            /*modelBuilder.Entity<ReparationVehicule>()
                  .HasKey(m => new { m.VehiculesId, m.ReparationsId});*/
            modelBuilder.Entity<Vehicule>()
                .HasMany(e => e.Reparations)
                .WithMany(e => e.Vehicules)
                .UsingEntity<ReparationVehicule>();
            
            modelBuilder.Entity<Reparation>()
                .HasMany(e => e.Vehicules)
                .WithMany(e => e.Reparations)
                .UsingEntity<ReparationVehicule>();
        }


        public DbSet<ExpressVoitures.Models.Vehicule> Vehicule { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Finition> Finition { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Marque> Marque { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Modele> Modele { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Reparation> Reparation { get; set; } = default!;
        //UPD06 add ReparationVehicule dbset (no dedicated controller // View)
        //  https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-8.0#composite-key
        public DbSet<ExpressVoitures.Models.ReparationVehicule> ReparationVehicule { get; set; } = default!;
        //UPD13 image support (dbset)
        public DbSet<Image> Images { get; set; }
    }
}
