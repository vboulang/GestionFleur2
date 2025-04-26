using System.Windows.Input;
using System.Windows;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows.Shapes;

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

		public AccueilViewModel()
		{
			UtilisateurEnConnexion = new Models.Utilisateur();
			GestionFleurContext GFContext = new GestionFleurContext();
			List<Models.Utilisateur> utilisateurs = GFContext.Utilisateurs.ToList();
			List<Models.Fleur> fleurs = GFContext.Fleurs.ToList();
			List<Models.Bouquet> bouquets = GFContext.Bouquets.ToList();
			if (fleurs.Count == 0)
				AddFlowerFromCSVInDB("../../../fleurs_db.csv");
			if (utilisateurs.Count == 0)
				AddUserFromApiInDB();
			if (bouquets.Count == 0)
				AddBouquetFromCSVInDB("../../../bouquets_db.csv");

			BoutonConnectionCommande = new RelayCommand(
					o => true,
					o => BoutonConnection()
				);
			BoutonInscriptionCommande = new RelayCommand(
					o => true,
					o => BoutonInscription()
				);

		}
		public void BoutonInscription()
		{
			Views.Inscription inscription = new Views.Inscription();
			inscription.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();

		}

		public void BoutonConnection()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			List<Models.Utilisateur> utilisateurs = GFContext.Utilisateurs.ToList();
			foreach (Models.Utilisateur util in utilisateurs)
			{
				if (UtilisateurEnConnexion.Identifiant == util.Identifiant && UtilisateurEnConnexion.MotDePasse == util.MotDePasse)
				{
					int id = util.UtilisateurId;
					switch (util.Type)
					{
						case "P":
							{
								Views.InterfaceProprietaire proprietaire = new Views.InterfaceProprietaire(id);
								proprietaire.Show();
								FermerFenetre();
								return;
							}
						case "V":
							{
								Views.InterfaceVendeur vendeur = new Views.InterfaceVendeur(id);
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
								Views.InterfaceClient client = new Views.InterfaceClient(id);
								client.Show();
								FermerFenetre();
								return;
							}
					}
				}
			}
			MessageBox.Show("Identifiant ou mot de passe incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
		}

		public void AddBouquetFromCSVInDB(string path)
		{
			using (var reader = new StreamReader(path))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				var records = csv.GetRecords<Models.Bouquet>();
				GestionFleurContext GFContext = new GestionFleurContext();

				foreach (var record in records)
				{
					Models.Bouquet nouveauBouquet = new Models.Bouquet();
					nouveauBouquet = record;
					nouveauBouquet.MessageCarte = "";
					GFContext.Bouquets.Add(nouveauBouquet);
					GFContext.SaveChanges();
					string[] TabFleurs = nouveauBouquet.FleursCSV.Split(";");
					int quantite = 0;
					foreach (string f in TabFleurs)
					{
						string[] FleursQt = f.Split();
						Models.Fleur fleurAAjouter = GFContext.Fleurs.FirstOrDefault(f => f.Nom == FleursQt[0]);
						if (FleursQt.Count() == 2)
							quantite = int.Parse(FleursQt[1]);
						if (fleurAAjouter != null)
						{
							nouveauBouquet.PrixUnitaire += Math.Round(fleurAAjouter.PrixUnitaire * quantite,2);
							GFContext.Bouquets.Update(nouveauBouquet);
							GFContext.SaveChanges();
							Models.FleursBouquets fleursBouquets = new Models.FleursBouquets();
							fleursBouquets.FleurId = fleurAAjouter.FleurId;
							fleursBouquets.BouquetId = nouveauBouquet.BouquetId;
							fleursBouquets.quantite = quantite;
							GFContext.FleursBouquets.Add(fleursBouquets);
							GFContext.SaveChanges();

						}
					}
				}
			}
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
					nouvellefleur.Quantite = 10;
					GFContext.Fleurs.Add(nouvellefleur);
					GFContext.SaveChanges();
				}
			}
		}

		public void AddUserFromApiInDB()
		{
			List<Models.Utilisateur> proprietaire = ApiQuery.GetUtilisateurs("users?limit=1&select=firstName,lastName,username,password");
			List<Models.Utilisateur> vendeurs = ApiQuery.GetUtilisateurs("users?limit=5&skip=1&select=firstName,lastName,username,password");
			List<Models.Utilisateur> fournisseurs = ApiQuery.GetUtilisateurs("users?limit=2&skip=6&select=firstName,lastName,username,password");
			List<Models.Utilisateur> clients = ApiQuery.GetUtilisateurs("users?limit=10&skip=8&select=firstName,lastName,username,password");
			foreach (Models.Utilisateur u in proprietaire)
				AddUtilisateurBD(u, "P");
			foreach (Models.Utilisateur u in vendeurs)
				AddUtilisateurBD(u, "V");
			foreach (Models.Utilisateur u in fournisseurs)
				AddUtilisateurBD(u, "F");
			foreach (Models.Utilisateur u in clients)
				AddUtilisateurBD(u, "C");
		}

		public void AddUtilisateurBD(Models.Utilisateur u, string type)
		{
			u.Type = type;
			GestionFleurContext GFContext = new GestionFleurContext();
			GFContext.Utilisateurs.Add(u);
			GFContext.SaveChanges();
		}
	}
}
