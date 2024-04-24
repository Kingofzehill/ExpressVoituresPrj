using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExpressVoitures.Data;
using System;
using System.Linq;
using Humanizer;
using Microsoft.VisualBasic;
using Mono.TextTemplating;


namespace ExpressVoitures.Models
{
    /// <summary>
    /// SeedData Class
    /// Initialize datas of the ExpressVoituresContext :
    /// Marque, Modele, Finition, Reparation, Vehicule, ReparationVehicule.
    /// </summary>
    /// <remarks>Users identites are managed in ApplicationDbContext.cs // SeedDataIdentity.cs.</remarks>
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
                // TD02 SeedData Vehicule
                if (!(context.Vehicule.Any()))
                {
                    context.Vehicule.AddRange(
                    new Vehicule
                    {
                        //Id = 1,
                        Vin = "VF7SBHMZ0EW554821",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "très bon état général",
                        Photo = null,
                        DateAchat = new DateTime(2023, 09, 10, 00, 00, 00),
                        PrixAchat =  11960.00M,
                        AnneeVehicule = 2013,
                        MisEnVente = true,
                        PrixDeVente = 12440.00M,
                        DateMisEnVente = new DateTime(2024, 03, 20, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        DateMisAJour = DateTime.Now,
                        Marge = 500.00M,
                        FinitionId = 1
                    }
                    );
                    context.SaveChanges();
                    /*
                    new Vehicule
                    {
                        //Id = 1,
                        Vin = "VF7SBHMZ0EW554821",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "très bon état général",
                        Photo = null,
                        DateAchat = new DateTime(2023, 09, 10, 00, 00, 00),
                        PrixAchat =  11960.00M,
                        AnneeVehicule = 2013,
                        MisEnVente = true,
                        PrixDeVente = 12440.00M,
                        DateMisEnVente = new DateTime(2024, 03, 20, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 1
                    ,
                    new Vehicule
                    {
                        //Id = 2,
                        Vin = "VF7SBHMZ0EW554822",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2023, 06, 12, 00, 00, 00),
                        PrixAchat = 15690.10M,
                        AnneeVehicule = 2016,
                        MisEnVente = true,
                        PrixDeVente = 16190.10M,
                        DateMisEnVente = new DateTime(2024, 03, 30, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 2
                    },
                    new Vehicule
                    {
                        //Id = 3,
                        Vin = "VF7SBHMZ0EW554823",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "Etat quasi neuf",
                        Photo = null,
                        DateAchat = new DateTime(2023, 12, 30, 00, 00, 00),
                        PrixAchat = 25450.00M,
                        AnneeVehicule = 2017,
                        MisEnVente = true,
                        PrixDeVente = 25950.00M,
                        DateMisEnVente = new DateTime(2024, 01, 25, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 3
                    },
                    new Vehicule
                    {
                        //Id = 4,
                        Vin = "VF7SBHMZ0EW554824",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2023, 06, 04, 00, 00, 00),
                        PrixAchat = 4475.00M,
                        AnneeVehicule = 2008,
                        MisEnVente = true,
                        PrixDeVente = 4975.00M,
                        DateMisEnVente = new DateTime(2024, 03, 10, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 4
                    },
                    new Vehicule
                    {
                        //Id = 5,
                        Vin = "VF7SBHMZ0EW554825",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2023, 11, 30, 00, 00, 00),
                        PrixAchat = 2599.99M,
                        AnneeVehicule = 2008,
                        MisEnVente = true,
                        PrixDeVente = 2999.99M,
                        DateMisEnVente = new DateTime(2024, 03, 26, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 5
                    },
                    new Vehicule
                    {
                        //Id = 6,
                        Vin = "VF7SBHMZ0EW554826",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2024, 02, 05, 00, 00, 00),
                        PrixAchat = 9400.00M,
                        AnneeVehicule = 2019,
                        MisEnVente = true,
                        PrixDeVente = 9900.00M,
                        DateMisEnVente = new DateTime(2024, 03, 18, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 6
                    },
                    new Vehicule
                    {
                        //Id = 7,
                        Vin = "VF7SBHMZ0EW554827",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2024, 01, 05, 00, 00, 00),
                        PrixAchat = 9400.00M,
                        AnneeVehicule = 2007,
                        MisEnVente = true,
                        PrixDeVente = 9900.00M,
                        DateMisEnVente = new DateTime(2024, 03, 02, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 7
                    },
                    new Vehicule
                    {
                        //Id = 8,
                        Vin = "VF7SBHMZ0EW554828",
                        Statut = VehiculeStatuts.EnVente,
                        Information = "",
                        Photo = null,
                        DateAchat = new DateTime(2024, 03, 04, 00, 00, 00),
                        PrixAchat = 6500.00M,
                        AnneeVehicule = 2010,
                        MisEnVente = true,
                        PrixDeVente = 7000.00M,
                        DateMisEnVente = new DateTime(2024, 03, 30, 00, 00, 00),
                        Vendu = false,
                        DateVente = null,
                        FinitionId = 4
                    }*/
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
