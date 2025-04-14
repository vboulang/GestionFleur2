using GestionFleur.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestionFleur.ViewModels
{
	internal class AccueilViewModel
	{

		//public Models.Greeting Greeting { get; set; }
		public ICommand BoutonConnectionCommande { get; private set; }
		public ICommand BoutonInscriptionCommande { get; private set; }
		public Models.Utilisateur Utilisateur { get; set; }
		public Action FermerFenetre { get; set; }


		public void BoutonInscription()
		{
			Views.Inscription ins = new Views.Inscription();
			ins.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();

		}

		public void BoutonConnection()
		{
			
		}


		public AccueilViewModel()
		{
			Utilisateur UtilisateurEnConnexion = new Utilisateur();
			BoutonConnectionCommande = new RelayCommand(
					o => true,
					o => BoutonConnection()
				);
			BoutonInscriptionCommande = new RelayCommand(
					o => true,
					o => BoutonInscription()
				);

		}
	}
}
