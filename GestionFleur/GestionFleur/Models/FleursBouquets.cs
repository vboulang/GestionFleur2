﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	public class FleursBouquets
	{
		[Key]
		public int BouquetFleursId { get; set; }

		[ForeignKey("FleurId")]
		public int FleurId { get; set; }
		public Fleur Fleur { get; set; }

		[ForeignKey("BouquetId")]
		public int BouquetId { get; set; }
		public Bouquet Bouquet { get; set; }

		public int quantite { get; set; }

	}
}
