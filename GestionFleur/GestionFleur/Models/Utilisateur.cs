﻿using GestionFleur.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Utilisateur
	{
		[JsonProperty("lastName")]
		public string Nom { get; set; }

		[JsonProperty("firstName")]
		public string Prenom { get; set; }
		[JsonProperty("username")]
		public string Identifiant { get; set; }
		
		[JsonProperty("password")]
		public string MotDePasse { get; set; }
		public string Type { get; set; }
		public int UtilisateurId { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public ICollection<Commande> CommandesClient { get; set; }
		public ICollection<Commande> CommandesASuperviser { get; set; }

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(Prenom) && !string.IsNullOrEmpty(Identifiant)
					&& !string.IsNullOrEmpty(MotDePasse) && !string.IsNullOrEmpty(Type);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
