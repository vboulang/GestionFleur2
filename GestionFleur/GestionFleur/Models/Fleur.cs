using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Fleur
	{
		public int FleurId { get; set; }
		public string nom { get; set; }
		public string couleur { get; set; }
		public string description{ get; set; }
		public int quantite { get; set; }

		//Liaison avec la table Bouquet
		public ICollection<Bouquet> Bouquets { get; set; }
		public ICollection<Commande> Commandes { get; set; }
	}
}
