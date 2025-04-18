using GestionFleur.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GestionFleur.ViewModels
{
	internal class InterfaceFournisseurViewModel
	{
		public ObservableCollection<Fleur> ToutesLesFleurs {  get; set; }
		Fleur FleurSelectionnee { get; set; }
		public Action FermerFenetre { get; set; }
		public ICommand BoutonRetourCommande { get; private set; }
		public ICommand EnleverFleurCommande { get; private set; }
		public ICommand AjouterFleurCommande { get; private set; }
		public InterfaceFournisseurViewModel()
		{
			GestionFleurContext GFContext = new GestionFleurContext();
			ToutesLesFleurs = new ObservableCollection<Fleur>(GFContext.Fleurs);
			BoutonRetourCommande = new RelayCommand(
				o => true,
				o => BoutonRetour()
			);
			AjouterFleurCommande = new RelayCommand(
				o => true,
				o => AjouterFleur()
			);
		}

		public void BoutonRetour()
		{
			Views.Accueil accueil = new Views.Accueil();
			accueil.Show(); // Affiche la nouvelle fenêtre
			FermerFenetre();
		}
		public void AjouterFleur()
		{
			MessageBox.Show(FleurSelectionnee.Nom + " a été ajouté à la commande");
			GestionFleurContext GFContext = new GestionFleurContext();
			if(FleurSelectionnee.QuantiteEnAttente > 0)
			{
				MessageBox.Show(FleurSelectionnee.Nom + " a été ajouté à la commande");
				FleurSelectionnee.Quantite += FleurSelectionnee.QuantiteEnAttente;
				FleurSelectionnee.QuantiteEnAttente = 0;
				GFContext.Fleurs.Update(FleurSelectionnee);
				GFContext.SaveChanges();
			}
			else
			{
				MessageBox.Show("Veuillez entrer une quantité valide");
			}
		}
	}
}
