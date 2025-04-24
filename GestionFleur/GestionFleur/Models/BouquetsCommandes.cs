using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	public class BouquetsCommandes
	{
		[Key]
		public int BouquetCommandeId { get; set; }

		[ForeignKey("BouquetId")]
		public int BouquetId { get; set; }
		public Bouquet Bouquet { get; set; }

		[ForeignKey("CommandeId")]
		public int CommandeId { get; set; }
		public Commande Commande { get; set; }

		public int quantite { get; set; }
	}
}
