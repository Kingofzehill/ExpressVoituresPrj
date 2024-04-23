using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpressVoitures.Data;
using System;
using System.Linq;


namespace ExpressVoitures.Models
{
    /// <summary>
    /// SeedData Class
    /// Initialize datas of the ExpressVoituresContext :
    /// Marque, Modele, Finition, Reparation, Vehicule, ReparationVehicule.
    /// </summary>
    /// <remarks>Users identites is managed in ApplicationDbContext.cs.</remarks>
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ExpressVoituresContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ExpressVoituresContext>>()))
            {
                // Look for any Marque.
                if (!(context.Marque.Any()))
                {                
                    context.Marque.AddRange(
                    new Marque
                    {
                        //Id = 1,
                        LibelleMarque = "Ford",                        
                    },                    
                    new Marque
                    {
                        //Id = 2,
                        LibelleMarque = "Volkswagen",
                    },
                    new Marque
                    {
                        //Id = 3,
                        LibelleMarque = "Honda",
                    },
                    new Marque
                    {
                        //Id = 4,
                        LibelleMarque = "Renault",
                    },
                    new Marque
                    {
                        //Id = 5,
                        LibelleMarque = "Mazda",
                    },
                    new Marque
                    {
                        //Id = 6,
                        LibelleMarque = "Jeep",
                    }
                    );
                    context.SaveChanges();
                }

                // Look for any Modele.
                if (!(context.Modele.Any()))
                {                
                    context.Modele.AddRange(
                    new Modele
                    {
                        //Id = 1,
                        LibelleModele = "Edge",
                        MarqueId = 1
                    },
                    new Modele
                    {
                        //Id = 2,
                        LibelleModele = "GTI",
                        MarqueId = 2
                    },
                    new Modele
                    {
                        //Id = 3,
                        LibelleModele = "Explorer",
                        MarqueId = 1
                    },
                    new Modele
                    {
                        //Id = 4,
                        LibelleModele = "Civic",
                        MarqueId = 3
                    },
                    new Modele
                    {
                        //Id = 5,
                        LibelleModele = "Scénic",
                        MarqueId = 4
                    },
                    new Modele
                    {
                        //Id = 6,
                        LibelleModele = "Miata",
                        MarqueId = 5
                    },
                    new Modele
                    {
                        //Id = 7,
                        LibelleModele = "Liberty",
                        MarqueId = 6
                    }
                    );
                    context.SaveChanges();
                }

                // Look for any Modele.
                if (!(context.Finition.Any()))
                {                
                    context.Finition.AddRange(
                    new Finition
                    {
                        //Id = 1,
                        LibelleFinition = "SEL",
                        ModeleId = 1
                    },
                    new Finition
                    {
                        //Id = 2,
                        LibelleFinition = "S",
                        ModeleId = 2
                    },
                    new Finition
                    {
                        //Id = 3,
                        LibelleFinition = "XLT",
                        ModeleId = 3
                    },
                    new Finition
                    {
                        //Id = 4,
                        LibelleFinition = "LX",
                        ModeleId = 4
                    },
                    new Finition
                    {
                        //Id = 5,
                        LibelleFinition = "TCe",
                        ModeleId = 5
                    },
                    new Finition
                    {
                        //Id = 6,
                        LibelleFinition = "LE",
                        ModeleId = 6
                    },
                    new Finition
                    {
                        //Id = 7,
                        LibelleFinition = "Sport",
                        ModeleId = 7
                    }
                    );
                    context.SaveChanges();
                }

                // Look for any Vehicule.
                if (!(context.Vehicule.Any()))
                {                
                    context.SaveChanges();
                }

                // Look for any Reparation.
                if (!(context.Reparation.Any()))                
                {
                    context.Reparation.AddRange(
                    new Reparation
                    {
                        //Id = 1,
                        LibelleReparation = "Restauration Complète",                        
                    },
                    new Reparation
                    {
                        //Id = 2,
                        LibelleReparation = "Roulements des roues avant",
                    },
                    new Reparation
                    {
                        //Id = 3,
                        LibelleReparation = "Radiateur",
                    },
                    new Reparation
                    {
                        //Id = 4,
                        LibelleReparation = "Freins",
                    },
                    new Reparation
                    {
                        //Id = 5,
                        LibelleReparation = "Pneus",
                    },
                    new Reparation
                    {
                        //Id = 6,
                        LibelleReparation = "Climatisation",
                    }
                    );
                    context.SaveChanges();
                }

                // Look for any ReparationVehicule.
                /* TD06 : why no dbset in ExpressVoituresContext.cs for ReparationVehicule
                 *     seedData for this association MtoM not possible due to that
                if (!(context.ReparationVehicule.Any()))                
                {

                }*/
            }
        }
    }
}
