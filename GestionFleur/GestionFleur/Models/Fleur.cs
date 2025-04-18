using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Fleur
	{

		//Nom,Prix Unitaire (CAD),Couleur,Caractéristiques
		[Ignore]
		public int FleurId { get; set; }

		[Name("Nom")]
		public string nom { get; set; }

		[Name("Couleur")]
		public string couleur { get; set; }

		[Name("Caractéristiques")]
		public string description{ get; set; }
		
		[Ignore]
		public int quantite { get; set; }
		
		[Name("Prix Unitaire (CAD)")]
		public double prixUnitaire { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;

		//Liaison avec la table Bouquet
		public ICollection<Bouquet> Bouquets { get; set; }
		public ICollection<Commande> Commandes { get; set; }
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(nom) && !string.IsNullOrEmpty(couleur) && !string.IsNullOrEmpty(description)
					&& (prixUnitaire > 0);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
