using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Fleur : INotifyPropertyChanged
	{
		private string _nom;
		private string _couleur;
		private string _description;
		private double _prixUnitaire;
		private int _quantite;
		private int _quantiteEnAttente;

		[Ignore]
		public int FleurId { get; set; }

		[Name("Nom")]
		public string Nom
		{
			get {  return _nom; }
			set
			{
				_nom = value;
				OnPropertyChanged();
			}
		}

		[Name("Couleur")]
		public string Couleur
		{
			get { return _couleur; }
			set
			{
				_couleur = value;
				OnPropertyChanged();
			}
		}

		[Name("Caractéristiques")]
		public string Description
		{
			get { return _description; }
			set
			{
				_description = value;
				OnPropertyChanged();
			}
		}

		[Ignore]
		[NotMapped]
		public int QuantiteEnAttente
		{
			get { return _quantiteEnAttente; }
			set
			{
				_quantiteEnAttente = value;
				OnPropertyChanged();
			}
		}

		[Ignore]
		public int Quantite
		{
			get { return _quantite; }
			set
			{
				_quantite = value;
				OnPropertyChanged();
			}
		}


		[Name("Prix Unitaire (CAD)")]
		public double PrixUnitaire
		{
			get { return _prixUnitaire;}
			set {
				_prixUnitaire = value;
				OnPropertyChanged();
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;

		//Liaison avec la table Bouquet
		public ICollection<FleursBouquets> Bouquets { get; set; }
		public ICollection<FleursCommandes> Commandes { get; set; }
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(Couleur) && !string.IsNullOrEmpty(Description)
					&&!(PrixUnitaire > 0);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
