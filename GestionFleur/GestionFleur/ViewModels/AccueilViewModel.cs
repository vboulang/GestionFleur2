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
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;

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
			GestionFleurContext GFContext = new GestionFleurContext();
			List<Utilisateur> utilisateurs = GFContext.Utilisateurs.ToList();
			foreach (Utilisateur util in utilisateurs)
			{
				if (UtilisateurEnConnexion.Identifiant == util.Identifiant && UtilisateurEnConnexion.MotDePasse == util.MotDePasse)
				{
					switch (util.Type)
					{
						case "P":
							{
								Views.InterfaceProprietaire proprietaire = new Views.InterfaceProprietaire();
								proprietaire.Show();
								FermerFenetre();
								return;
							}
						case "V":
							{
								Views.InterfaceVendeur vendeur = new Views.InterfaceVendeur();
								vendeur.Show();
								FermerFenetre();
								return;
							}
						case "F":
							{
								Views.InterfaceFournisseur fournisseur = new Views.InterfaceFournisseur();
								fournisseur.Show();
								FermerFenetre();
								return;
							}
						case "C":
							{
								Views.InterfaceClient client = new Views.InterfaceClient();
								client.Show();
								FermerFenetre();
								return;
							}
					}
				}
			}
			MessageBox.Show("Identifiant ou mot de passe incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
		}


		public AccueilViewModel()
		{
			UtilisateurEnConnexion = new Models.Utilisateur();
			GestionFleurContext GFContext = new GestionFleurContext();
			List<Utilisateur> utilisateurs = GFContext.Utilisateurs.ToList();
			List<Fleur> fleurs = GFContext.Fleurs.ToList();

			if (utilisateurs.Count == 0)
				AddUserFromApiInDB();
			if (fleurs.Count == 0)
				AddFlowerFromCSVInDB("../../../fleurs_db.csv");

			BoutonConnectionCommande = new RelayCommand(
					o => true,
					o => BoutonConnection()
				);
			BoutonInscriptionCommande = new RelayCommand(
					o => true,
					o => BoutonInscription()
				);

		}

		public void AddFlowerFromCSVInDB(string path)
		{
			using (var reader = new StreamReader(path))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				var records = csv.GetRecords<Models.Fleur>();
				Models.Fleur nouvellefleur = new Models.Fleur();
				GestionFleurContext GFContext = new GestionFleurContext();

				foreach (var record in records)
				{
					nouvellefleur = record;
					nouvellefleur.quantite = 10;
					GFContext.Fleurs.Add(nouvellefleur);
					GFContext.SaveChanges();
				}
			}
		}

		public void AddUserFromApiInDB()
		{
			List<Utilisateur> proprietaire = ApiQuery.GetUtilisateurs("users?limit=1&select=firstName,lastName,username,password");
			List<Utilisateur> vendeurs = ApiQuery.GetUtilisateurs("users?limit=5&skip=1&select=firstName,lastName,username,password");
			List<Utilisateur> fournisseurs = ApiQuery.GetUtilisateurs("users?limit=2&skip=6&select=firstName,lastName,username,password");
			List<Utilisateur> clients = ApiQuery.GetUtilisateurs("users?limit=10&skip=8&select=firstName,lastName,username,password");
			foreach (Utilisateur u in proprietaire)
				AddUtilisateurBD(u, "P");
			foreach (Utilisateur u in vendeurs)
				AddUtilisateurBD(u, "V");
			foreach (Utilisateur u in fournisseurs)
				AddUtilisateurBD(u, "F");
			foreach (Utilisateur u in clients)
				AddUtilisateurBD(u, "C");
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
