using GestionFleur.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace GestionFleur.ViewModels
{
	internal class AccueilViewModel
	{

		//public Models.Greeting Greeting { get; set; }
		public ICommand BoutonConnectionCommande { get; private set; }
		public ICommand BoutonInscriptionCommande { get; private set; }
		public Models.Utilisateur UtilisateurEnConnexion { get; set; }
		public Action FermerFenetre { get; set; }

		public RestApiQuery ApiQuery = new RestApiQuery();


		public void BoutonInscription()
		{
			Views.Inscription inscription = new Views.Inscription();
			inscription.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();

		}

		public void BoutonConnection()
		{
			
		}


		public AccueilViewModel()
		{
			UtilisateurEnConnexion = new Models.Utilisateur();
			GestionFleurContext GFContext = new GestionFleurContext();
			List<Utilisateur> utilisateurs = GFContext.Utilisateurs.ToList();
			if(utilisateurs.Count == 0)
				AddUserFromApiInDB();

			BoutonConnectionCommande = new RelayCommand(
					o => true,
					o => BoutonConnection()
				);
			BoutonInscriptionCommande = new RelayCommand(
					o => true,
					o => BoutonInscription()
				);

		}

		public void AddUserFromApiInDB()
		{
			List<Utilisateur> proprietaire = ApiQuery.GetUtilisateurs("users?limit=1&select=firstName,lastName,username,password,id");
			List<Utilisateur> vendeurs = ApiQuery.GetUtilisateurs("users?limit=5&skip=1&select=firstName,lastName,username,password,id");
			List<Utilisateur> fournisseurs = ApiQuery.GetUtilisateurs("users?limit=2&skip=6&select=firstName,lastName,username,password,id");
			List<Utilisateur> clients = ApiQuery.GetUtilisateurs("users?limit=10&skip=8&select=firstName,lastName,username,password,id");
			foreach (Utilisateur u in proprietaire)
				AddUtilisateurBD(u, "P");
			foreach (Utilisateur u in vendeurs)
				AddUtilisateurBD(u, "V");
			foreach (Utilisateur u in fournisseurs)
				AddUtilisateurBD(u, "F");
			foreach (Utilisateur u in clients)
				AddUtilisateurBD(u, "C");
			
		UtilisateurEnConnexion.MotDePasse = clients.Count.ToString();
		}

		public void AddUtilisateurBD(Utilisateur u, string type)
		{
			u.Type = type;
			GestionFleurContext GFContext = new GestionFleurContext();
			GFContext.Utilisateurs.Add(u);
			GFContext.SaveChanges();
		}
	}
}
