using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Commande : INotifyPropertyChanged
	{
		public int CommandeId { get; set; }
		public double TotalTransaction { get; set; }
		public TypeDePaiement TypeDePaiement { get; set; }
		public bool PaiementEffectue { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		//Liaison avec la table Bouquet
		public ICollection<FleursCommandes> Fleurs { get; set; }
		public ICollection<BouquetsCommandes> Bouquets { get; set; }

		public int ClientId { get; set; }
		public Utilisateur Client { get; set; }

		public int VendeurId { get; set; }
		public Utilisateur Vendeur { get; set; }
		public bool IsValid()
		{
			return !(TotalTransaction >= 0) && !(TypeDePaiement != null);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
