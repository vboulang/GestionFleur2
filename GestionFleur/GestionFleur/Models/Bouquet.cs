using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Bouquet : INotifyPropertyChanged
	{
		private string _nom;
		private string _fleursCSV;
		private string _messageCarte;
		private double _prixUnitaire;
		private int _quantiteEnAttente;

		[Ignore]
		public int BouquetId { get; set; }
		[Name("Nom")]
		public string Nom 
		{  
			get { return _nom; }
			set
			{
				_nom = value;
				OnPropertyChanged();
			}
		}
		[Name("Fleurs")]
		[NotMapped]
		public string FleursCSV { get; set; }
		[Ignore]
		public string MessageCarte 
		{
			get { return _messageCarte; }
			set
			{
				_messageCarte = value;
				OnPropertyChanged();
			}
		}
		[Ignore]
		public double PrixUnitaire 
		{ 
			get { return _prixUnitaire; }
			set
			{
				_prixUnitaire = Math.Round(value,2);
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
		public event PropertyChangedEventHandler PropertyChanged;

		//Liaison avec la table Bouquet
		public ICollection<FleursBouquets> Fleurs { get; set; }
		public ICollection<BouquetsCommandes> Commandes { get; set; }

		public bool IsValid()
		{
			return PrixUnitaire > 0; //&& !string.IsNullOrEmpty(MessageCarte);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
