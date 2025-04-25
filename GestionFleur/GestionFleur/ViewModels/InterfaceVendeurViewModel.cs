using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GestionFleur.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GestionFleur.ViewModels
{
	internal class InterfaceVendeurViewModel
	{
		public int UtilisateurEnConnexionId { get; set; }
		public Models.ListeCommande ListeCommandes { get; set; }
		public ICommand SupprimerCommandeCommand { get; private set; }
		public ICommand GenererFactureCommand { get; private set; }
		public ICommand BoutonRetourCommande { get; private set; }
		public Action FermerFenetre { get; set; }
		private GestionFleurContext _gestionFleurContext;

		public InterfaceVendeurViewModel(int utilId)
		{
			_gestionFleurContext = new GestionFleurContext();
			ListeCommandes = new Models.ListeCommande();
			UtilisateurEnConnexionId = utilId;
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
		}


		//fonctionnel mail il reste a update en meme temps le paiementefffectuer
		public void GenererFacture(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			List<FleursCommandes> fleursCommandes = _gestionFleurContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<BouquetsCommandes> bouquetsCommandes = _gestionFleurContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			string facture = "Facture de la commande " + CommandeSelectionnee.CommandeId + "\n";
			
			if(fleursCommandes.Count() != 0)
			{
				facture += "Fleurs commandées :\n";
				foreach (FleursCommandes fc in fleursCommandes)
				{
					Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
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
				foreach (BouquetsCommandes bc in bouquetsCommandes)
				{
					Bouquet bouquet = _gestionFleurContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
					List<FleursBouquets> fleursbouquets = _gestionFleurContext.FleursBouquets.Where(f => f.BouquetId == bouquet.BouquetId).ToList();
					foreach (FleursBouquets fb in fleursbouquets)
					{
						Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					}
					facture += bc.quantite + " " + bouquet.Nom + " à " + Math.Round(bouquet.PrixUnitaire, 2) + "$\n";
				}
			}
			else
			{
				facture += "Aucun bouquet commandé\n";
			}
			
			facture += "Total : " + Math.Round(CommandeSelectionnee.TotalTransaction, 2) + "$\n";
			Utilisateur vendeur = _gestionFleurContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == UtilisateurEnConnexionId);
			facture += "Vendu par : " + vendeur.Prenom + " " + "" + vendeur.Nom + " (" + UtilisateurEnConnexionId + ")\n";
			facture += "Merci de votre achat !\n";
			MessageBox.Show(facture, "Facture");
			Commande CommandeAnnulee = _gestionFleurContext.Commandes.FirstOrDefault(c => c.CommandeId == CommandeSelectionnee.CommandeId);
			if (CommandeAnnulee.PaiementEffectue == false)
			{
				CommandeAnnulee.PaiementEffectue = true;
				_gestionFleurContext.Commandes.Update(CommandeAnnulee);
				_gestionFleurContext.SaveChanges();
			}
		}

		//fonctionnelle uniquement s<il ny a pas de bouquet dedans
		public void SupprimerCommande(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			List<FleursCommandes> fleursCommandes = _gestionFleurContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<BouquetsCommandes> bouquetsCommandes = _gestionFleurContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			foreach (FleursCommandes fc in fleursCommandes)
			{
				Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
				fleur.Quantite += fc.quantite;
				_gestionFleurContext.Fleurs.Update(fleur);
				_gestionFleurContext.SaveChanges();
			}
			foreach (BouquetsCommandes bc in bouquetsCommandes)
			{
				Bouquet bouquet = _gestionFleurContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
				List<FleursBouquets> fleursbouquets = _gestionFleurContext.FleursBouquets.Where(f => f.BouquetId == bouquet.BouquetId).ToList();
				foreach (FleursBouquets fb in fleursbouquets)
				{
					Fleur fleur = _gestionFleurContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					fleur.Quantite += bc.quantite * fb.quantite;
					_gestionFleurContext.Fleurs.Update(fleur);
					_gestionFleurContext.SaveChanges();
				}
			}
			_gestionFleurContext.Commandes.Remove(CommandeSelectionnee);
			_gestionFleurContext.SaveChanges();
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(_gestionFleurContext.Commandes.ToList());
		}
		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show();
			FermerFenetre();
		}
	}
}
