using GestionFleur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionFleur.ViewModels
{
	internal class InterfaceClientViewModel
	{
		public Models.Commande CommandePresente { get; set; }
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
		public InterfaceClientViewModel()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			Fleurs = new ObservableCollection<Fleur>(GFContext.Fleurs.ToList());
			Bouquets = new ObservableCollection<Bouquet>(GFContext.Bouquets.ToList());
			FleursCommandees = new ObservableCollection<Fleur>();
			BouquetsCommandes = new ObservableCollection<Bouquet>();
			CommandePresente = new Models.Commande();
			Commandes = new ObservableCollection<Models.Commande>();
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
			//if (Commande.IsValid())
			//{
				//Commandes.Add(Commande);
				//Commande = new Models.Commande();
			//}
		}
		public void AjouterFleurCommande(Object fleur)
		{
			Fleur fleurAAjouter = (Fleur)fleur;
			GestionFleurContext GFContext = new GestionFleurContext();
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
			Bouquet bouquetAAjouter = (Bouquet)bouquet;
			GestionFleurContext GFContext = new GestionFleurContext();
			foreach (Bouquet bouquetInCollection in BouquetsCommandes)
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
