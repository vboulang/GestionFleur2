using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Client : Utilisateur
	{
		public int ClientId { get; set; }
		public ICollection<Commande> Commandes { get; set; }
	}
}
