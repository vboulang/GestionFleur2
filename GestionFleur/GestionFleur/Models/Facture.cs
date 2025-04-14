using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Facture
	{
		public int FactureId { get; set; }
		public double TotalTransaction { get; set; }
		public TypeDePaiement TypeDePaiement { get; set; }
		public bool paiementEffectue { get; set; }
		
	}
}
