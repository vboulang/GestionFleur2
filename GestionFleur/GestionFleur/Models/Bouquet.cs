﻿using CsvHelper.Configuration.Attributes;
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
		[Ignore]
		public int BouquetId { get; set; }
		[Name("Nom")]
		public string Nom {  get; set; }
		[Name("Fleurs")]
		[NotMapped]
		public string FleursCSV { get; set; }
		[Ignore]
		public string MessageCarte {  get; set; }
		[Ignore]
		public double PrixUnitaire { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;

		//Liaison avec la table Bouquet
		public ICollection<FleursBouquets> Fleurs { get; set; }
		public ICollection<BouquetsCommandes> Commandes { get; set; }

		public bool IsValid()
		{
			return !(PrixUnitaire > 0) && !string.IsNullOrEmpty(MessageCarte);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
