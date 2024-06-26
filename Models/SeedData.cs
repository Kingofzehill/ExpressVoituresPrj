﻿using ExpressVoitures.Data;
using Microsoft.EntityFrameworkCore;


namespace ExpressVoitures.Models
{
    /// <summary>
    /// SeedData Class
    /// Initialize datas of the ExpressVoituresContext :
    /// Marque, Modele, Finition, Reparation, Vehicule, ReparationVehicule.
    /// </summary>
    /// <remarks>Vehicules images not managed.</remarks>
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            try
            {
                // Method form Learn Microsoft : https://learn.microsoft.com/fr-fr/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-8.0&tabs=visual-studio
                using (var context = new ExpressVoituresContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ExpressVoituresContext>>()))
                {
                    // Look for any Vehicule.
                    if (context.Vehicule.Any())
                    {
                        return;   // DB has been seeded.
                    }

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
                        context.Vehicule.AddRange(
                        new Vehicule
                        {
                            //Id = 1, 2013 Ford Edge SEL
                            Vin = "VF7SBHMZ0EW554821",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Très bon état général. Pneus et freins neufs. Recharge climtisation.",                            
                            DateAchat = new DateTime(2023, 09, 10, 00, 00, 00),
                            PrixAchat = 10990.00M,
                            AnneeVehicule = 2013,
                            MisEnVente = true,
                            PrixDeVente = 12440.00M,
                            DateMisEnVente = new DateTime(2024, 03, 20, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Pneus, freins, climatisation",
                            CoutReparations = 950.00M,
                            Marge = 500.00M,
                            FinitionId = 1,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 2, 2016 Volkswagen GTI S
                            Vin = "VF7SBHMZ0EW554822",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Pneus neufs.",                            
                            DateAchat = new DateTime(2023, 06, 12, 00, 00, 00),
                            PrixAchat = 15250.10M,
                            AnneeVehicule = 2016,
                            MisEnVente = true,
                            PrixDeVente = 16190.10M,
                            DateMisEnVente = new DateTime(2024, 03, 30, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Pneus",
                            CoutReparations = 440.00M,
                            Marge = 500.00M,
                            FinitionId = 2,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 3, 2017 Ford Explorer XLT
                            Vin = "VF7SBHMZ0EW554823",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Etat quasi neuf.",                            
                            DateAchat = new DateTime(2023, 12, 30, 00, 00, 00),
                            PrixAchat = 24350.00M,
                            AnneeVehicule = 2017,
                            MisEnVente = true,
                            PrixDeVente = 25950.00M,
                            DateMisEnVente = new DateTime(2024, 01, 25, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Pneus, freins",
                            CoutReparations = 1100.00M,
                            Marge = 500.00M,
                            FinitionId = 3,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 4, 2008 Honda Civic LX
                            Vin = "VF7SBHMZ0EW554824",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Freins neufs. Recharge climatisation.",                            
                            DateAchat = new DateTime(2023, 06, 04, 00, 00, 00),
                            PrixAchat = 4000.00M,
                            AnneeVehicule = 2008,
                            MisEnVente = true,
                            PrixDeVente = 4975.00M,
                            DateMisEnVente = new DateTime(2024, 03, 10, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "climatisation, freins",
                            CoutReparations = 475.00M,
                            Marge = 500.00M,
                            FinitionId = 4,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 5, 2017 Renault Scénic TCe
                            Vin = "VF7SBHMZ0EW554825",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Bon état général. Freins neufs, remplacement radiateur.",                            
                            DateAchat = new DateTime(2023, 11, 30, 00, 00, 00),
                            PrixAchat = 1809.99M,
                            AnneeVehicule = 2008,
                            MisEnVente = true,
                            PrixDeVente = 2999.99M,
                            DateMisEnVente = new DateTime(2024, 03, 26, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Radiateur, freins",
                            CoutReparations = 690.00M,
                            Marge = 500.00M,
                            FinitionId = 5,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 6, 2019 Mazda Miata LE
                            Vin = "VF7SBHMZ0EW554826",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Occasion à saisir. Restauration complète. Pack Sport.",                            
                            DateAchat = new DateTime(2024, 02, 05, 00, 00, 00),
                            PrixAchat = 1800.00M,
                            AnneeVehicule = 2019,
                            MisEnVente = true,
                            PrixDeVente = 9900.00M,
                            DateMisEnVente = new DateTime(2024, 03, 18, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Restauration complète",
                            CoutReparations = 7600.00M,
                            Marge = 500.00M,
                            FinitionId = 6,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 7, 2007 Jeep Liberty Sport
                            Vin = "VF7SBHMZ0EW554827",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Changement des roulements roues avant.",
                            DateAchat = new DateTime(2024, 01, 05, 00, 00, 00),
                            PrixAchat = 4500.00M,
                            AnneeVehicule = 2007,
                            MisEnVente = true,
                            PrixDeVente = 5350.00M,
                            DateMisEnVente = new DateTime(2024, 03, 02, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "Roulements des roues avant",
                            CoutReparations = 350.00M,
                            Marge = 500.00M,
                            FinitionId = 7,
                            Indisponible = false
                        },
                        new Vehicule
                        {
                            //Id = 8, 2010 Honda Civic Lx
                            Vin = "VF7SBHMZ0EW554828",
                            Statut = VehiculeStatuts.EnVente,
                            Information = "Faible kilométrage (54 000 km).",
                            DateAchat = new DateTime(2024, 03, 04, 00, 00, 00),
                            PrixAchat = 6500.00M,
                            AnneeVehicule = 2010,
                            MisEnVente = true,
                            PrixDeVente = 7000.00M,
                            DateMisEnVente = new DateTime(2024, 03, 30, 00, 00, 00),
                            Vendu = false,
                            DateVente = null,
                            DateMisAJour = DateTime.Now,
                            listeReparations = "",
                            CoutReparations = 0,
                            Marge = 500.00M,
                            FinitionId = 4,
                            Indisponible = false
                        }
                        );
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
                    /* TD06 : ReparationVehicule : as there is no Controller (and so dbset in ExpressVoituresContext.cs) 
                     *     seedData for this association MtoM not possible due to that*/
                    if (!(context.ReparationVehicule.Any()))
                    {
                        var ReparationVehicules = new ReparationVehicule[]
                        {
                            // 2013 Ford Edge SEL 
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554821").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Pneus").Id
                            },
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554821").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Freins").Id
                            },
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554821").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Climatisation").Id
                            },
                            // 2016 Volkswagen GTI S 
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554822").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Pneus").Id
                            },
                            // 2017 Ford Explorer XLT
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554823").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Pneus").Id
                            },
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554823").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Freins").Id
                            },
                            // 2008 Honda Civic LX
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554824").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Climatisation").Id
                            },
                             new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554824").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Freins").Id
                            },
                            // 2017 Renault Scénic TCe
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554825").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Radiateur").Id,
                            },
                             new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554825").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Freins").Id
                            },
                            // 2019 Mazda Miata LE
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554826").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Restauration Complète").Id
                            },
                            //2007 Jeep Liberty Sport
                            new ReparationVehicule
                            {
                                VehiculesId = context.Vehicule.Single(c => c.Vin == "VF7SBHMZ0EW554827").Id,
                                ReparationsId = context.Reparation.Single(i => i.LibelleReparation == "Roulements des roues avant").Id
                            }
                        };

                        foreach (ReparationVehicule rv in ReparationVehicules)
                        {
                            context.ReparationVehicule.Add(rv);
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SeedData failure...");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
