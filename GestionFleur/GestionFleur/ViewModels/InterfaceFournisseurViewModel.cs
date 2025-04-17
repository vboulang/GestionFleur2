using GestionFleur.Models;
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
	internal class InterfaceFournisseurViewModel
	{
		public ObservableCollection<Fleur> ToutesLesFleurs {  get; set; }
		public Action FermerFenetre { get; set; }
		private ICommand BoutonRetourCommande;
		InterfaceFournisseurViewModel()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			ToutesLesFleurs = new ObservableCollection<Fleur>(GFContext.Fleurs);
			BoutonRetourCommande = new RelayCommand(
				o => true,
				o => BoutonRetour()
			);
		}

		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}
	}
}
