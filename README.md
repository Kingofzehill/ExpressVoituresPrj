Express Voitures - 2024
Application web ASP .NET Core réalisé avec le framework .NET 8 et architecture MCV (Modèle Vue Contrôleur).
Développée par Stéphane Moureu.

Instruction d'installation :
- Dupliquer (fork) le projet depuis le dépôt distant Git Hub de l'application vers votre dépôt distant.
- Cloner localement le code source de l'application depuis votre dépôt distant.
- Installer le framework .NET 8 : https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Installer l'IDE (Environnement de développement intégré) Microsoft Visual Studio Community 2022 (64 bits) 
      _https://visualstudio.microsoft.com/fr/vs/community/_
      _Version 17.9.5 utilisée pour le projet._
      _IDE extensible, complet et gratuit pour créer des applications modernes pour Android, iOS, Windows, ainsi que
      des applications web et des services cloud._
- Installer SQL Server 2022
      _https://www.microsoft.com/fr-fr/sql-server/sql-server-2022_
      _Moteur de base de données SGBD/R (Système de gestion de base de données relationnelles)._
- Installer SQL Server Management Studio
      _Version 19.2 utilisée pour le projet._
      _https://learn.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16_

  
Instruction de démarrage (DEVELOPPEMENT) :
- Ouvrir le projet dans Visual Studio en pointant vers les sources de l'application clonée depuis votre dépôt distant GitHub,
  fichier : ExpressVoitures.sln.
  
- Créer la base de données depuis Visual Studio.
      _Menu : Affichage / Autres fenêtres / Console du gestionnaire de package_
      L'application se connecte à deux Bases de données via les connections string suivantes (fichier appsettings.json) :
      - Connection String de la base de données des Identités Utilisateurs : "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-ExpressVoitures-04774a4e-5a92-4d58-b43e-bbf6a2f33fc3;Trusted_Connection=True;MultipleActiveResultSets=true",
      - Connection String de la base de données des Véhicules : "ExpressVoituresContext": "Server=(localdb)\\mssqllocaldb;Database=ExpressVoituresContext-fef897d5-e94d-4717-98f9-dc396e42519f;Trusted_Connection=True;MultipleActiveResultSets=true"
      
      Dans la console du gestionnaire de package, taper les commandes suivantes pour créer la base des données Véhicules :      
      - Update-database -Context ExpressVoituresContext
      _L'application inclue des scripts créant un jeu d'essai et l'accès administrateur (voir ../Data/Seeddata(...).cs)
      au premier lancement de l'application_
  
- Démarrer l'application en la lançant en débug dans Visual Studio (menu Déboger ==> Démarrer le débogage).
  
- L'application s'ouvre dans sa version pour accès public.
      _Pour se connecter en tant qu'administrateur et ainsi accéder au menu d'administration, cliquer sur le
      lien Connexion en haut à droite et connecter vous avec les identifiant / mot de passe administrateur fournis._
  
- Pour administrer la base de données, vous pouvez passer par Visual Studio (menu : Affichage / Explorateur d'objets SQL Server).
  Nous conseillons d'utiliser SQL Server Management Studio (SMSS) pour administer la base de données.
    Lancer SMSS.
    Cliquer sur Connecter.
    Nom du serveur : (localdb)\mssqllocaldb
    Authentification : Authentification Windows.

    La base de données _aspnet-ExpressVoitures-04774a4e-5a92-4d58-b43e-bbf6a2f33fc3_ contient les identités utilisateurs (administrateur).
    Voir table dbo.AspNetUsers.
    La base de données _ExpressVoituresContext-fef897d5-e94d-4717-98f9-dc396e42519f_ contient les données Véhicules de l'application.
    
  
