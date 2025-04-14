using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Bouquet : Article
	{
		public string BouquetId {  get; set; }
		public string MessageCarte {  get; set; }
		
		//Liaison avec la table Bouquet
		public ICollection<Fleur> Fleurs { get; set; }
		public ICollection<Commande> Commandes { get; set; }
	}
}
