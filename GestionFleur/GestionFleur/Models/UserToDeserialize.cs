using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class UserToDeserialize
	{
		public List<Utilisateur> users { get; set; }
	}
}
