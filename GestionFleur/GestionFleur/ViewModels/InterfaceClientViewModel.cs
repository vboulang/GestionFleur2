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
		public string MessageCarte { get; set; }
		public int UtilisateurEnConnexionId { get; set; }
		public Models.Bouquet BouquetPerso { get; set; }
		public Models.ListeCommande ListeCommandes { get; set; }
		public Models.Commande CommandePresente { get; set; }
		public ObservableCollection<Models.Fleur> Fleurs { get; set; }
		public ObservableCollection<Models.Fleur> FleursAAjouterBouquet { get; set; }
		public ObservableCollection<Models.Bouquet> Bouquets { get; set; }
		public ObservableCollection<Models.Fleur> FleursCommandees { get; set; }
		public ObservableCollection<Models.Bouquet> BouquetsCommandes { get; set; }
		public ICommand AjouterCommandeCommand { get; private set; }
		public ICommand SupprimerCommandeCommand { get; private set; }
		public ICommand ReinitialiserCommandeCommand { get; private set; }
		public ICommand BoutonRetourCommande { get; private set; }
		public ICommand ReinitialiserBouquetCommand { get; private set; }
		public ICommand AjouterFleurCommandeCommand { get; private set; }
		public ICommand AjouterFleurBouquetCommand { get; private set; }
		public ICommand AjouterBouquetCommandeCommand { get; private set; }
		public ICommand CreerBouquetPersoCommand { get; private set; }
		private GestionFleurContext _gestionFleurContext;
		public Action FermerFenetre { get; set; }
		public InterfaceClientViewModel(int utilId)
		{
			_gestionFleurContext = new GestionFleurContext();
			ListeCommandes = new Models.ListeCommande();
			UtilisateurEnConnexionId = utilId;
			Fleurs = new ObservableCollection<Models.Fleur>(_gestionFleurContext.Fleurs.ToList());
			Bouquets = new ObservableCollection<Models.Bouquet>(_gestionFleurContext.Bouquets.Where(b=>b.BouquetId <= 15).ToList());
			FleursCommandees = new ObservableCollection<Models.Fleur>();
			BouquetsCommandes = new ObservableCollection<Models.Bouquet>();
			FleursAAjouterBouquet = new ObservableCollection<Models.Fleur>();
			CommandePresente = new Models.Commande();
			BouquetPerso = new Models.Bouquet();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(_gestionFleurContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());
			AjouterCommandeCommand = new RelayCommand(
				o=>FleursCommandees.Count() > 0 || BouquetsCommandes.Count() > 0,
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
			CreerBouquetPersoCommand = new RelayCommand(
				o => BouquetPerso.IsValid() && FleursAAjouterBouquet.Count()>0,
				o => CreerBouquet()
				);
			AjouterBouquetCommandeCommand = new RelayCommand(
				o => true,
				bouquet => AjouterBouquetCommande(bouquet)
				);
			SupprimerCommandeCommand = new RelayCommand(
				o=>true,
				commande=>SupprimerCommande(commande)
				);
			ReinitialiserCommandeCommand = new RelayCommand(
				o=>true,
				o=>ReinitialiserCommande()
				);
			ReinitialiserBouquetCommand = new RelayCommand(
				o => true,
				o => ReinitialiserBouquet()
				);
			BoutonRetourCommande = new RelayCommand(
				o => true,
				o => BoutonRetour()
				);
		}
		public void AjouterCommande()
		{
			Models.Commande CommandePresente = new Models.Commande();
			Models.Utilisateur UtilisateurEnConnexion = _gestionFleurContext.Utilisateurs.FirstOrDefault(c => c.UtilisateurId == UtilisateurEnConnexionId);
			CommandePresente.Client = UtilisateurEnConnexion;
			CommandePresente.TotalTransaction = 0;
			_gestionFleurContext.Commandes.Add(CommandePresente);
			_gestionFleurContext.SaveChanges();
			if (FleursCommandees.Count > 0)
			{
				foreach (Models.Fleur fleurInCollection in FleursCommandees)
				{
					CommandePresente.TotalTransaction += Math.Round(fleurInCollection.PrixUnitaire * fleurInCollection.QuantiteEnAttente, 2);
					Models.FleursCommandes fleursCommandes = new Models.FleursCommandes();
					fleursCommandes.FleurId = fleurInCollection.FleurId;
					fleursCommandes.CommandeId = CommandePresente.CommandeId;
					fleursCommandes.quantite = fleurInCollection.QuantiteEnAttente;
					_gestionFleurContext.FleursCommandes.Add(fleursCommandes);
					_gestionFleurContext.SaveChanges();
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
					_gestionFleurContext.BouquetsCommandes.Add(bouquetsCommandes);
					_gestionFleurContext.SaveChanges();
					List<Models.FleursBouquets> fleursInBouquet = _gestionFleurContext.FleursBouquets.Where(f => f.BouquetId == bouquetInCollection.BouquetId).ToList();
					foreach (Models.FleursBouquets fleur in fleursInBouquet)
					{
						Models.Fleur f = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fleur.FleurId);
						f.Quantite -= bouquetInCollection.QuantiteEnAttente * fleur.quantite;
						_gestionFleurContext.SaveChanges();
					}
					bouquetInCollection.QuantiteEnAttente = 0;
				}
			}
			_gestionFleurContext.SaveChanges();
			FleursCommandees.Clear();
			BouquetsCommandes.Clear();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(_gestionFleurContext.Commandes.Where(c => c.ClientId == UtilisateurEnConnexionId).ToList());
		}
		public void AjouterFleurCommande(Object fleur)
		{
			Models.Fleur fleurAAjouter = (Models.Fleur)fleur;
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
		public void AjouterFleurBouquet(Object fleur)
		{
			Models.Fleur fleurAAjouter = (Models.Fleur)fleur;
			foreach (Models.Fleur fleurInCollection in FleursAAjouterBouquet)
			{
				if (fleurInCollection.Nom == fleurAAjouter.Nom)
				{
					fleurInCollection.QuantiteEnAttenteBPerso += 1;
					return;
				}
			}
			fleurAAjouter.QuantiteEnAttenteBPerso += 1;
			FleursAAjouterBouquet.Add(fleurAAjouter);
		}
		public void CreerBouquet()
		{
			BouquetPerso.MessageCarte = BouquetPerso.TempMessageCarte;
			BouquetPerso.Nom = "Bouquet personnalisé";
			BouquetPerso.QuantiteEnAttente += 1;
			_gestionFleurContext.Bouquets.Add(BouquetPerso);
			_gestionFleurContext.SaveChanges();
			foreach (Models.Fleur fleurInCollection in FleursAAjouterBouquet)
			{
				BouquetPerso.PrixUnitaire += Math.Round(fleurInCollection.PrixUnitaire * fleurInCollection.QuantiteEnAttenteBPerso, 2);
				Models.FleursBouquets fleursBouquets = new Models.FleursBouquets();
				fleursBouquets.BouquetId = BouquetPerso.BouquetId;
				fleursBouquets.FleurId = fleurInCollection.FleurId;
				fleursBouquets.quantite = fleurInCollection.QuantiteEnAttenteBPerso;
				fleurInCollection.QuantiteEnAttenteBPerso = 0;
				_gestionFleurContext.FleursBouquets.Add(fleursBouquets);
				_gestionFleurContext.SaveChanges();
			}
			BouquetPerso.TempMessageCarte = "";

			BouquetsCommandes.Add(BouquetPerso);
			_gestionFleurContext.SaveChanges();
			FleursAAjouterBouquet.Clear();
			BouquetPerso = new Models.Bouquet();
		}
		public void AjouterBouquetCommande(Object bouquet)
		{
			Models.Bouquet bouquetAAjouter = (Models.Bouquet)bouquet;
			foreach (Models.Bouquet bouquetInCollection in BouquetsCommandes)
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
		public void SupprimerCommande(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande) commande;
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
		public void ReinitialiserBouquet()
		{
			foreach (Models.Fleur fleurInCollection in FleursAAjouterBouquet)
			{
				if (fleurInCollection.QuantiteEnAttenteBPerso > 0)
				{
					fleurInCollection.QuantiteEnAttenteBPerso = 0;
				}
			}
			FleursAAjouterBouquet.Clear();
			BouquetPerso.PrixUnitaire = 0;
			BouquetPerso.MessageCarte = "";
			BouquetPerso.TempMessageCarte = "";
			BouquetPerso = new Models.Bouquet();
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
			FleursCommandees.Clear();
			BouquetsCommandes.Clear();
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
