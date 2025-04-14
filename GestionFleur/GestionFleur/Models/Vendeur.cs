using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Vendeur : Utilisateur
	{
		public int VendeurId { get; set; }
		public ICollection<Commande> Commandes { get; set; }
	}
}
