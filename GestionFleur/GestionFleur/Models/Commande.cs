using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Commande : INotifyPropertyChanged
	{
		[Key]
		public int CommandeId { get; set; }

		public double TotalTransaction { get; set; }
		public TypeDePaiement TypeDePaiement { get; set; }
		private bool _paiementEffectue;
		public bool PaiementEffectue
		{
			get { return _paiementEffectue; }
			set
			{
				_paiementEffectue = value;
				OnPropertyChanged();
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		//Liaison avec la table Bouquet
		public ICollection<FleursCommandes> Fleurs { get; set; }
		public ICollection<BouquetsCommandes> Bouquets { get; set; }

		public int ClientId { get; set; }
		public Utilisateur Client { get; set; }

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
