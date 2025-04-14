using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Commande
	{
		public int CommandeId { get; set; }
		//Liaison avec la table Bouquet
		public ICollection<Fleur> Fleurs { get; set; }
		public ICollection<Bouquet> Bouquets { get; set; }

		public int ClientId { get; set; }
		public Client Client { get; set; }

		public int VendeursId { get; set; }
		public Vendeur Vendeur { get; set; }

	}
}
