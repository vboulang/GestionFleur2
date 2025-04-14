using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class FleursBouquets
	{
		[Key]
		public int BouquetCommandeId { get; set; }

		[ForeignKey("FleurId")]
		public Fleur Fleur { get; set; }

		[ForeignKey("BouquetId")]
		public Bouquet Bouquet { get; set; }

	}
}
