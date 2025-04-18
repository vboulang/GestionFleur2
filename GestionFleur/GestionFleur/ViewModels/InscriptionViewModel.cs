using GestionFleur.Models;
using System.Windows;
using System.Windows.Input;

namespace GestionFleur.ViewModels
{
	internal class InscriptionViewModel
	{
		public ICommand BoutonInscriptionCommande { get; private set; }
		public Models.Utilisateur NouvelUtilisateur { get; set; }
		public ICommand BoutonRetourCommande { get; private set; }
		public Action FermerFenetre { get; set; }

		public InscriptionViewModel()
		{
			NouvelUtilisateur = new Models.Utilisateur();
			BoutonInscriptionCommande = new RelayCommand(
				o => NouvelUtilisateur.IsValid(),
				o => BoutonInscription()
			);
			BoutonRetourCommande = new RelayCommand(
					o => true,
					o => BoutonRetour()
				);
		}

		public void BoutonInscription()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			Utilisateur utilisateurPresent = GFContext.Utilisateurs.FirstOrDefault(u => u.Identifiant == NouvelUtilisateur.Identifiant);
			if(utilisateurPresent != null )
				MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
			else
			{
				Utilisateur nouvelUtilisateurdb = new Utilisateur();
				nouvelUtilisateurdb.Nom = NouvelUtilisateur.Nom;
				nouvelUtilisateurdb.Prenom = NouvelUtilisateur.Prenom;
				nouvelUtilisateurdb.Identifiant = NouvelUtilisateur.Identifiant;
				nouvelUtilisateurdb.MotDePasse = NouvelUtilisateur.MotDePasse;
				nouvelUtilisateurdb.Type = NouvelUtilisateur.Type;
				GFContext.Utilisateurs.Add(nouvelUtilisateurdb);
				NouvelUtilisateur.Nom = "";
				NouvelUtilisateur.Prenom = "";
				NouvelUtilisateur.Identifiant = "";
				NouvelUtilisateur.MotDePasse = "";
				NouvelUtilisateur.Type = "";
				MessageBox.Show("Utilisateur créé avec succès", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

			}
			GFContext.SaveChanges();
		}

		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}

	}
}
