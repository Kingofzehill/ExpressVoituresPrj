using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpressVoitures.Models;

namespace ExpressVoitures.Data
{
    public class ExpressVoituresContext : DbContext
    {
        public ExpressVoituresContext (DbContextOptions<ExpressVoituresContext> options)
            : base(options)
        {
        }

        public DbSet<ExpressVoitures.Models.Vehicule> Vehicule { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Finition> Finition { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Marque> Marque { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Modele> Modele { get; set; } = default!;
        public DbSet<ExpressVoitures.Models.Reparation> Reparation { get; set; } = default!;
    }
}
