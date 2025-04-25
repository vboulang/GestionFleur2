using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestionFleur.ViewModels
{
	internal class InterfaceClientViewModel
	{
		public int UtilisateurEnConnexionId { get; set; }
		public Models.ListeCommande ListeCommandes { get; set; }
		public Models.Commande CommandePresente { get; set; }
		//public ObservableCollection<Models.Commande> Commandes { get; set; }
		public ObservableCollection<Models.Fleur> Fleurs { get; set; }
		public ObservableCollection<Models.Bouquet> Bouquets { get; set; }
		public ObservableCollection<Models.Fleur> FleursCommandees { get; set; }
		public ObservableCollection<Models.Bouquet> BouquetsCommandes { get; set; }
		public ICommand AjouterCommandeCommand { get; private set; }
		public ICommand SupprimerCommandeCommand { get; private set; }
		public ICommand ReinitialiserCommandeCommand { get; private set; }
		public ICommand BoutonRetourCommande { get; private set; }
		public ICommand AjouterFleurCommandeCommand { get; private set; }
		public ICommand AjouterFleurBouquetCommand { get; private set; }
		public ICommand AjouterBouquetCommandeCommand { get; private set; }

		public Action FermerFenetre { get; set; }
		public InterfaceClientViewModel(int utilId)
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			ListeCommandes = new Models.ListeCommande();
			UtilisateurEnConnexionId = utilId;
			Fleurs = new ObservableCollection<Models.Fleur>(GFContext.Fleurs.ToList());
			Bouquets = new ObservableCollection<Models.Bouquet>(GFContext.Bouquets.ToList());
			FleursCommandees = new ObservableCollection<Models.Fleur>();
			BouquetsCommandes = new ObservableCollection<Models.Bouquet>();
			CommandePresente = new Models.Commande();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(GFContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());
			AjouterCommandeCommand = new RelayCommand(
				o=>true,
				o=>AjouterCommande()
				);
			AjouterFleurCommandeCommand = new RelayCommand(
				o => true,
				fleur => AjouterFleurCommande(fleur)
				);
			AjouterFleurBouquetCommand = new RelayCommand(
				o => true,
				o => AjouterFleurBouquet()
				);
			AjouterBouquetCommandeCommand = new RelayCommand(
				o => true,
				bouquet => AjouterBouquetCommande(bouquet)
				);
			SupprimerCommandeCommand = new RelayCommand(
				o=>true,
				o=>SupprimerCommande()
				);
			ReinitialiserCommandeCommand = new RelayCommand(
				o=>true,
				o=>ReinitialiserCommande()
				);
			BoutonRetourCommande = new RelayCommand(
				o => true,
				o => BoutonRetour()
				);
		}
		public void AjouterCommande()
		{
			Models.Commande CommandePresente = new Models.Commande();
			GestionFleurContext GFContext = new GestionFleurContext();
			Models.Utilisateur UtilisateurEnConnexion = GFContext.Utilisateurs.FirstOrDefault(c => c.UtilisateurId == UtilisateurEnConnexionId);
			CommandePresente.Client = UtilisateurEnConnexion;
			CommandePresente.TotalTransaction = 0;
			GFContext.Commandes.Add(CommandePresente);
			GFContext.SaveChanges();
			if (FleursCommandees.Count > 0)
			{
				foreach (Models.Fleur fleurInCollection in FleursCommandees)
				{
					CommandePresente.TotalTransaction += Math.Round(fleurInCollection.PrixUnitaire * fleurInCollection.QuantiteEnAttente, 2);
					Models.FleursCommandes fleursCommandes = new Models.FleursCommandes();
					fleursCommandes.FleurId = fleurInCollection.FleurId;
					fleursCommandes.CommandeId = CommandePresente.CommandeId;
					fleursCommandes.quantite = fleurInCollection.QuantiteEnAttente;
					GFContext.FleursCommandes.Add(fleursCommandes);
					GFContext.SaveChanges();
					fleurInCollection.Quantite -= fleurInCollection.QuantiteEnAttente;
					fleurInCollection.QuantiteEnAttente = 0;
				}
			}
			if (BouquetsCommandes.Count > 0)
			{
				foreach (Models.Bouquet bouquetInCollection in BouquetsCommandes)
				{
					CommandePresente.TotalTransaction += Math.Round(bouquetInCollection.PrixUnitaire * bouquetInCollection.QuantiteEnAttente, 2);
					Models.BouquetsCommandes bouquetsCommandes = new Models.BouquetsCommandes();
					bouquetsCommandes.BouquetId = bouquetInCollection.BouquetId;
					bouquetsCommandes.CommandeId = CommandePresente.CommandeId;
					bouquetsCommandes.quantite = bouquetInCollection.QuantiteEnAttente;
					GFContext.BouquetsCommandes.Add(bouquetsCommandes);
					GFContext.SaveChanges();
					List<Models.FleursBouquets> fleursInBouquet = GFContext.FleursBouquets.Where(f => f.BouquetId == bouquetInCollection.BouquetId).ToList();
					foreach (Models.FleursBouquets fleur in fleursInBouquet)
					{
						Models.Fleur f = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fleur.FleurId);
						f.Quantite -= bouquetInCollection.QuantiteEnAttente * fleur.quantite;
						GFContext.SaveChanges();
					}
					bouquetInCollection.QuantiteEnAttente = 0;
				}
			}
			GFContext.SaveChanges();
			FleursCommandees.Clear();
			BouquetsCommandes.Clear();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(GFContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());
			MessageBox.Show(ListeCommandes.Commandes.Count().ToString());
		}
		public void AjouterFleurCommande(Object fleur)
		{
			Models.Fleur fleurAAjouter = (Models.Fleur)fleur;
			GestionFleurContext GFContext = new GestionFleurContext();
			foreach(Models.Fleur fleurInCollection in FleursCommandees)
			{
				if (fleurInCollection.Nom == fleurAAjouter.Nom)
				{
					fleurInCollection.QuantiteEnAttente += 1;
					return;
				}
			}
			fleurAAjouter.QuantiteEnAttente += 1;
			FleursCommandees.Add(fleurAAjouter);
		}
		public void AjouterFleurBouquet()
		{
			//if (Commande.IsValid())
			//{
			//Commandes.Add(Commande);
			//Commande = new Models.Commande();
			//}
		}
		public void AjouterBouquetCommande(Object bouquet)
		{
			Models.Bouquet bouquetAAjouter = (Models.Bouquet)bouquet;
			GestionFleurContext GFContext = new GestionFleurContext();
			foreach (Models.Bouquet bouquetInCollection in BouquetsCommandes)
			{
				if (bouquetInCollection.Nom == bouquetAAjouter.Nom)
				{
					CommandePresente.TotalTransaction += bouquetAAjouter.PrixUnitaire;
					bouquetInCollection.QuantiteEnAttente += 1;
					return;
				}
			}
			bouquetAAjouter.QuantiteEnAttente += 1;
			BouquetsCommandes.Add(bouquetAAjouter);
			CommandePresente.TotalTransaction += bouquetAAjouter.PrixUnitaire;
		}
		public void SupprimerCommande()
		{
			//Commande = new Models.Commande();
		}
		public void ReinitialiserCommande()
		{
			foreach (Models.Fleur fleurInCollection in FleursCommandees)
			{
				if (fleurInCollection.QuantiteEnAttente > 0)
				{
					fleurInCollection.QuantiteEnAttente = 0;
				}
			}
			foreach (Models.Bouquet bouquetInCollection in BouquetsCommandes)
			{
				if (bouquetInCollection.QuantiteEnAttente > 0)
				{
					bouquetInCollection.QuantiteEnAttente = 0;
				}
			}
			int i = FleursCommandees.Count() - 1;
			while(i >= 0)
			{
				FleursCommandees.Remove(FleursCommandees[i]);
				i--;
			}
			i = BouquetsCommandes.Count() - 1;
			while (i >= 0)
			{
				BouquetsCommandes.Remove(BouquetsCommandes[i]);
				i--;
			}
			CommandePresente.TotalTransaction = 0;
			CommandePresente = new Models.Commande();
		}
		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}
	}
}
