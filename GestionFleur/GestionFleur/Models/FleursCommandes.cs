using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	public class FleursCommandes
	{
		[Key]
		public int FleurCommandeId { get; set; }

		[ForeignKey("FleurId")]
		public int FleurId { get; set; }
		public Fleur Fleur { get; set; }

		[ForeignKey("CommandeId")]
		public int CommandeId { get; set; }

		public Commande Commande { get; set; }
		public int quantite { get; set; }
	}
}
