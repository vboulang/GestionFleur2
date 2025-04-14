using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Bouquet
	{
		public string BouquetId {  get; set; }
		public string MessageCarte {  get; set; }
		public double PrixUnitaire { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;

		//Liaison avec la table Bouquet
		public ICollection<Fleur> Fleurs { get; set; }
		public ICollection<Commande> Commandes { get; set; }

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
