Express Voitures - 2024
Application web ASP .NET Core réalisée avec le framework .NET 8 et architecture MCV (_Modèle Vue Contrôleur_).
Développée par Stéphane Moureu.

Instruction d'installation :
- Dupliquer (_fork_) le projet depuis le dépôt distant Git Hub de l'application vers votre dépôt distant.
- Cloner localement le code source de l'application depuis votre dépôt distant.
- Installer le framework .NET 8 : https://dotnet.microsoft.com/en-us/download/dotnet/8.0.
- Installer l'IDE (_Environnement de développement intégré_) Microsoft Visual Studio Community 2022 (_64 bits_), version 17.9.5 utilisée pour le projet.
    - Lien : https://visualstudio.microsoft.com/fr/vs/community/.
- Installer SQL Server 2022 (_Moteur de base de données SGBD/R (Système de gestion de base de données relationnelles)_).
    - Lien : https://www.microsoft.com/fr-fr/sql-server/sql-server-2022      
- Installer SQL Server Management Studio, version 19.2 utilisée pour le projet.
    - Lien : https://learn.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16        
  
Instruction de démarrage (_DEVELOPPEMENT_) :
- Ouvrir le projet dans Visual Studio (_fichier : ExpressVoitures.sln_).
- Information bases de données :
    - L'application se connecte à deux Bases de données via les connections string suivantes (_fichier appsettings.json_).
    - Connection String de la base de données des Identités Utilisateurs : _"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-ExpressVoitures-04774a4e-5a92-4d58-b43e-bbf6a2f33fc3;Trusted_Connection=True;MultipleActiveResultSets=true"_.
    - Connection String de la base de données des Voitures : _"ExpressVoituresContext": "Server=(localdb)\\mssqllocaldb;Database=ExpressVoituresContext-fef897d5-e94d-4717-98f9-dc396e42519f;Trusted_Connection=True;MultipleActiveResultSets=true"_.
- Dans la console du gestionnaire de package (_Menu : Affichage / Autres fenêtres / Console du gestionnaire de package_), taper les commandes suivantes pour créer la base des données Véhicules :      
    - add-migration InitLocalDB_01
    - Update-database -Context ExpressVoituresContext
_L'application inclut des scripts créant un jeu d'essai et l'accès administrateur (_voir ../Data/Seeddata(...).cs_) au premier lancement de l'application.
- Démarrer l'application en la lançant en débug dans Visual Studio (_menu Déboger ==> Démarrer le débogage_), l'application s'ouvre en accès grand public (_pour se connecter en tant qu'administrateur et accéder au menu d'administration, cliquer sur le lien Connexion en haut à droite et connecter vous avec les identifiant / mot de passe administrateur fournis_).
- Pour administrer la base de données, vous pouvez passer par Visual Studio (_menu : Affichage / Explorateur d'objets SQL Server_). Nous conseillons d'utiliser SQL Server Management Studio (_SMSS_) pour administer la base de données.
    - Lancer SMSS.
    - Cliquer sur Connecter.
        - Nom du serveur : (localdb)\mssqllocaldb
        - Authentification : Authentification Windows.
