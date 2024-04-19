namespace ExpressVoitures.Models
{
    //Enumération des statuts possibles d'un Vehicule
    //      https://www.bytehide.com/blog/enum-csharp
    //      ** ENum in model classes https://education.launchcode.org/csharp-web-dev-curriculum/enums/reading/enums-in-models/index.html
    //      var currentStatut = VehiculeStatuts.Achat.ToString(); // renvoie une string du statut
    public enum VehiculeStatuts
    {
        Achat,
        ReparationAFaire,
        ReparationEnCours,
        EnVente,
        Vendu,
        Indisponible
    }
}
