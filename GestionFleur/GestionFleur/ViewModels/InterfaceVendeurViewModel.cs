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

		public InterfaceVendeurViewModel(int utilId)
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			ListeCommandes = new Models.ListeCommande();
			UtilisateurEnConnexionId = utilId;
			ListeCommandes.Commandes = new ObservableCollection<Models.Commande>(GFContext.Commandes.ToList());
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

		public void GenererFacture(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			GestionFleurContext GFContext = new GestionFleurContext();
			List<FleursCommandes> fleursCommandes = GFContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<BouquetsCommandes> bouquetsCommandes = GFContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			string facture = "Facture de la commande " + CommandeSelectionnee.CommandeId + "\n";
			facture += "Fleurs commandées :\n";
			foreach (FleursCommandes fc in fleursCommandes)
			{
				Fleur fleur = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
				facture += fc.quantite + " " + fleur.Nom + " à " + Math.Round(fleur.PrixUnitaire, 2) + "$\n";
			}
			facture += "Bouquets commandés :\n";
			foreach (BouquetsCommandes bc in bouquetsCommandes)
			{
				Bouquet bouquet = GFContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
				foreach (FleursBouquets fb in bouquet.Fleurs)
				{
					Fleur fleur = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					facture += bc.quantite + " " + bouquet.Nom + " à " + Math.Round(bouquet.PrixUnitaire, 2) + "$\n";
				}
			}
			facture += "Total : " + Math.Round(CommandeSelectionnee.TotalTransaction, 2) + "$\n";
			Utilisateur vendeur =  GFContext.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == UtilisateurEnConnexionId);
			facture += "Vendu par : " + vendeur.Nom + "(" + UtilisateurEnConnexionId + ")";
			facture += "Merci de votre achat !\n";
			MessageBox.Show(facture, "Facture");
			CommandeSelectionnee.PaiementEffectue = true;
			GFContext.Commandes.Update(CommandeSelectionnee);
		}

		public void SupprimerCommande(Object commande)
		{
			Models.Commande CommandeSelectionnee = (Models.Commande)commande;
			GestionFleurContext GFContext = new GestionFleurContext();
			List<FleursCommandes> fleursCommandes = GFContext.FleursCommandes.Where(f => f.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			List<BouquetsCommandes> bouquetsCommandes = GFContext.BouquetsCommandes.Where(b => b.CommandeId == CommandeSelectionnee.CommandeId).ToList();
			foreach (FleursCommandes fc in fleursCommandes)
			{
				Fleur fleur = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fc.FleurId);
				fleur.Quantite += fc.quantite;
				GFContext.Fleurs.Update(fleur);
				GFContext.SaveChanges();
			}
			foreach (BouquetsCommandes bc in bouquetsCommandes)
			{
				Bouquet bouquet = GFContext.Bouquets.FirstOrDefault(b => b.BouquetId == bc.BouquetId);
				foreach (FleursBouquets fb in bouquet.Fleurs)
				{
					Fleur fleur = GFContext.Fleurs.FirstOrDefault(f => f.FleurId == fb.FleurId);
					fleur.Quantite += bc.quantite * fb.quantite;
					GFContext.Fleurs.Update(fleur);
					GFContext.SaveChanges();
				}
			}
			GFContext.Commandes.Remove(CommandeSelectionnee);
			GFContext.SaveChanges();
		}
		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show();
			FermerFenetre();
		}
	}
}
