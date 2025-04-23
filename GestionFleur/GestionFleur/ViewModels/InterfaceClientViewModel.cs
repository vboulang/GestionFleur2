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
		public Models.Commande Commande { get; set; }
		public ObservableCollection<Models.Commande> Commandes { get; set; }
		public ObservableCollection<Fleur> Fleurs { get; set; }
		public ObservableCollection<Bouquet> Bouquets { get; set; }
		public ICommand AjouterCommandeCommand { get; private set; }
		public ICommand SupprimerCommandeCommand { get; private set; }
		public ICommand ReinitialiserCommandeCommand { get; private set; }
		public InterfaceClientViewModel()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			Fleurs = new ObservableCollection<Fleur>(GFContext.Fleurs.ToList());
			Bouquets = new ObservableCollection<Bouquet>(GFContext.Bouquets.ToList());

			Commande = new Models.Commande();
			Commandes = new ObservableCollection<Models.Commande>();
			AjouterCommandeCommand = new RelayCommand(
				o=>true,
				o=>AjouterCommande()
				);
			SupprimerCommandeCommand = new RelayCommand(
				o=>true,
				o=>SupprimerCommande()
				);
			ReinitialiserCommandeCommand = new RelayCommand(
				o=>true,
				o=>ReinitialiserCommande()
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
		public void SupprimerCommande()
		{
			//if (Commandes.Count > 0)
			//{
				//Commandes.RemoveAt(Commandes.Count - 1);
			//}
		}
		public void ReinitialiserCommande()
		{
			//Commande = new Models.Commande();
		}
	}
}
