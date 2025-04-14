using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class BouquetsCommandes
	{
		[Key]
		public int BouquetCommandeId { get; set; }

		[ForeignKey("BouquetId")]
		public Bouquet Bouquet { get; set; }

		[ForeignKey("CommandeId")]
		public Commande Commande { get; set; }
	}
}
