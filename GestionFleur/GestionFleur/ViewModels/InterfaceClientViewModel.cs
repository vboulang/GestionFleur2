using GestionFleur.Models;
using Microsoft.IdentityModel.Tokens;
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
		public Models.Commande CommandePresente { get; set; }
		public Models.Bouquet NouveauBouquet { get; set; }
		public ObservableCollection<Models.Commande> Commandes { get; set; }
		public ObservableCollection<Fleur> Fleurs { get; set; }
		public ObservableCollection<Bouquet> Bouquets { get; set; }
		public ObservableCollection<Fleur> FleursCommandees { get; set; }
		public ObservableCollection<Bouquet> BouquetsCommandes { get; set; }
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
			UtilisateurEnConnexionId = utilId;
			Fleurs = new ObservableCollection<Fleur>(GFContext.Fleurs.ToList());
			Bouquets = new ObservableCollection<Bouquet>(GFContext.Bouquets.ToList());
			FleursCommandees = new ObservableCollection<Fleur>();
			BouquetsCommandes = new ObservableCollection<Bouquet>();
			NouveauBouquet = new Models.Bouquet();
			Commandes = new ObservableCollection<Models.Commande>(GFContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());

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
				fleur => AjouterFleurBouquet(fleur)
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
				foreach (Fleur fleurInCollection in FleursCommandees)
				{
					CommandePresente.TotalTransaction += Math.Round(fleurInCollection.PrixUnitaire * fleurInCollection.QuantiteEnAttente,2);
					FleursCommandes fleursCommandes = new FleursCommandes();
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
				foreach (Bouquet bouquetInCollection in BouquetsCommandes)
				{
					CommandePresente.TotalTransaction += Math.Round(bouquetInCollection.PrixUnitaire * bouquetInCollection.QuantiteEnAttente,2);
					BouquetsCommandes bouquetsCommandes = new BouquetsCommandes();
					bouquetsCommandes.BouquetId = bouquetInCollection.BouquetId;
					bouquetsCommandes.CommandeId = CommandePresente.CommandeId;
					bouquetsCommandes.quantite = bouquetInCollection.QuantiteEnAttente;
					GFContext.BouquetsCommandes.Add(bouquetsCommandes);
					GFContext.SaveChanges();
					List<FleursBouquets> fleursInBouquet = GFContext.FleursBouquets.Where(f => f.BouquetId == bouquetInCollection.BouquetId).ToList();
					foreach (FleursBouquets fleur in fleursInBouquet)
					{
						MessageBox.Show(fleur.quantite.ToString());
						Fleur f = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fleur.FleurId);
						f.Quantite -= bouquetInCollection.QuantiteEnAttente * fleur.quantite;
						GFContext.SaveChanges();
					}
					bouquetInCollection.QuantiteEnAttente = 0;
				}
			}
			GFContext.SaveChanges();
			FleursCommandees.Clear();
			BouquetsCommandes.Clear();
			Commandes = new ObservableCollection<Models.Commande>(GFContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());
		}
		public void AjouterFleurCommande(Object fleur)
		{
			Fleur fleurAAjouter = (Fleur)fleur;
			foreach(Fleur fleurInCollection in FleursCommandees)
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
		public void AjouterFleurBouquet(Object fleur)
		{
			Fleur fleurAAjouter = (Fleur)fleur;
			//foreach (Fleur fleurInCollection in FleursCommandees)
			//{
			//	if (fleurInCollection.Nom == fleurAAjouter.Nom)
			//	{
			//		fleurInCollection.QuantiteEnAttente += 1;
			//		return;
			//	}
			//}
			//fleurAAjouter.QuantiteEnAttente += 1;
			//NouveauBouquet.Fleurs.Add(fleur);
		}
		public void AjouterBouquetCommande(Object bouquet)
		{
			Bouquet bouquetAAjouter = (Bouquet)bouquet;
			foreach (Bouquet bouquetInCollection in BouquetsCommandes)
			{
				if (bouquetInCollection.Nom == bouquetAAjouter.Nom)
				{
					bouquetInCollection.QuantiteEnAttente += 1;
					return;
				}
			}
			bouquetAAjouter.QuantiteEnAttente += 1;
			BouquetsCommandes.Add(bouquetAAjouter);
		}
		public void SupprimerCommande()
		{
			//Commande = new Models.Commande();
		}
		public void ReinitialiserCommande()
		{
			foreach (Fleur fleurInCollection in FleursCommandees)
			{
				if (fleurInCollection.QuantiteEnAttente > 0)
				{
					fleurInCollection.QuantiteEnAttente = 0;
				}
			}
			foreach (Bouquet bouquetInCollection in BouquetsCommandes)
			{
				if (bouquetInCollection.QuantiteEnAttente > 0)
				{
					bouquetInCollection.QuantiteEnAttente = 0;
				}
			}
			FleursCommandees.Clear();
			BouquetsCommandes.Clear();
		}
		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}
	}
}
