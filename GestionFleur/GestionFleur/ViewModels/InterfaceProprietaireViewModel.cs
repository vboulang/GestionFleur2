using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using GestionFleur.Models;

namespace GestionFleur.ViewModels
{
	internal class InterfaceProprietaireViewModel
	{
		public int UtilisateurEnConnexionId { get; set; }
		public ObservableCollection<Models.Fleur> ToutesLesFleurs { get; set; }
		public Action FermerFenetre { get; set; }
		public Models.ListeCommande ListeCommandes { get; set; }

		public ICommand BoutonRetourCommande { get; private set; }
		public ICommand SupprimerCommandeCommand { get; private set; }
		public ICommand GenererFactureCommand { get; private set; }
		public ICommand AjouterFleurCommande { get; private set; }
		private GestionFleurContext _gestionFleurContext;

		public InterfaceProprietaireViewModel(int utilId)
		{
			UtilisateurEnConnexionId = utilId;
			ListeCommandes = new Models.ListeCommande();
			_gestionFleurContext = new GestionFleurContext();
			ToutesLesFleurs = new ObservableCollection<Models.Fleur>(_gestionFleurContext.Fleurs);
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(_gestionFleurContext.Commandes.ToList());

			SupprimerCommandeCommand = new RelayCommand(
				o => true,
				commande => SupprimerCommande(commande)
				);
			GenererFactureCommand = new RelayCommand(
				o => true,
				commande => GenererFacture(commande)
				);
			BoutonRetourCommande = new RelayCommand(
				o => true,
				o => BoutonRetour()
			);
			AjouterFleurCommande = new RelayCommand(
				o => true,
				fleur => AjouterFleur(fleur)
			);
		}

		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}
		public void AjouterFleur(Object fleur)
		{
			Models.Fleur FleurSelectionnee = (Models.Fleur)fleur;

			if (FleurSelectionnee.QuantiteEnAttente > 0)
			{
				MessageBox.Show(FleurSelectionnee.Nom + " a été ajouté à la commande");
				FleurSelectionnee.Quantite += FleurSelectionnee.QuantiteEnAttente;
				FleurSelectionnee.QuantiteEnAttente = 0;
				_gestionFleurContext.Fleurs.Update(FleurSelectionnee);
				_gestionFleurContext.SaveChanges();
			}
			else
			{
				MessageBox.Show("Veuillez entrer une quantité valide");
			}
		}
		public void GenererFacture(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			List<Models.FleursCommandes> fleursCommandes = _gestionFleurContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<Models.BouquetsCommandes> bouquetsCommandes = _gestionFleurContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			string facture = "Facture de la commande " + CommandeSelectionnee.CommandeId + "\n";
			if (fleursCommandes.Count() != 0)
			{
				facture += "Fleurs commandées :\n";
				foreach (Models.FleursCommandes fc in fleursCommandes)
				{
					Models.Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
					facture += fc.quantite + " " + fleur.Nom + " à " + Math.Round(fleur.PrixUnitaire, 2) + "$\n";
				}
			}
			else
			{
				facture += "Aucune fleur individuelle commandée\n";
			}

			if (bouquetsCommandes.Count() != 0)
			{
				facture += "Bouquets commandés :\n";
				foreach (Models.BouquetsCommandes bc in bouquetsCommandes)
				{
					Models.Bouquet bouquet = _gestionFleurContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
					List<Models.FleursBouquets> fleursbouquets = _gestionFleurContext.FleursBouquets.Where(f => f.BouquetId == bouquet.BouquetId).ToList();
					foreach (Models.FleursBouquets fb in fleursbouquets)
					{
						Models.Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					}
					facture += bc.quantite + " " + bouquet.Nom + " à " + Math.Round(bouquet.PrixUnitaire, 2) + "$\n";
				}
			}
			else
			{
				facture += "Aucun bouquet commandé\n";
			}

			facture += "Total : " + Math.Round(CommandeSelectionnee.TotalTransaction, 2) + "$\n";
			Models.Utilisateur vendeur = _gestionFleurContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == UtilisateurEnConnexionId);
			facture += "Vendu par : " + vendeur.Prenom + " " + "" + vendeur.Nom + " (" + UtilisateurEnConnexionId + ")\n";
			facture += "Merci de votre achat !\n";
			CommandeSelectionnee.PaiementEffectue = true;
			_gestionFleurContext.Commandes.Update(CommandeSelectionnee);
			_gestionFleurContext.SaveChanges();
			MessageBox.Show(facture, "Facture");
		}

		//fonctionnelle uniquement s<il ny a pas de bouquet dedans
		public void SupprimerCommande(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			List<Models.FleursCommandes> fleursCommandes = _gestionFleurContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<Models.BouquetsCommandes> bouquetsCommandes = _gestionFleurContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			foreach (Models.FleursCommandes fc in fleursCommandes)
			{
				Models.Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
				fleur.Quantite += fc.quantite;
				_gestionFleurContext.Fleurs.Update(fleur);
				_gestionFleurContext.SaveChanges();
			}
			foreach (Models.BouquetsCommandes bc in bouquetsCommandes)
			{
				Models.Bouquet bouquet = _gestionFleurContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
				List<Models.FleursBouquets> fleursbouquets = _gestionFleurContext.FleursBouquets.Where(f => f.BouquetId == bouquet.BouquetId).ToList();
				foreach (Models.FleursBouquets fb in fleursbouquets)
				{
					Models.Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					fleur.Quantite += bc.quantite * fb.quantite;
					_gestionFleurContext.Fleurs.Update(fleur);
					_gestionFleurContext.SaveChanges();
				}
			}
			_gestionFleurContext.Commandes.Remove(CommandeSelectionnee);
			_gestionFleurContext.SaveChanges();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(_gestionFleurContext.Commandes.ToList());
		}
	}
}
