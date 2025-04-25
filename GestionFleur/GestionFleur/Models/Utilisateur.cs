using GestionFleur.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GestionFleur.Models
{
	internal class Utilisateur : INotifyPropertyChanged
	{
		private string _nom;
		private string _prenom;
		private string _motDePasse;
		private string _identifiant;


		[JsonProperty("lastName")]
		public string Nom
		{
			get { return _nom; }
			set
			{
				if (_nom != value)
				{
					_nom = value;
					OnPropertyChanged();
				}
			}
		}

		[JsonProperty("firstName")]
		public string Prenom
		{ 
			get { return _prenom; }
			set
			{
				if (_prenom != value)
				{
					_prenom = value;
					OnPropertyChanged();
				}
			}
		}
		
		[JsonProperty("username")]
		public string Identifiant
		{
			get { return _identifiant; }
			set
			{
				if (_identifiant != value)
				{
					_identifiant = value;
					OnPropertyChanged();
				}
			}
		}
		
		[JsonProperty("password")]
		public string MotDePasse
		{
			get { return _motDePasse; }
			set
			{
				if (_motDePasse != value)
				{
					_motDePasse = value;
					OnPropertyChanged();
				}
			}
		}
		public string Type{ get; set;}
		public int UtilisateurId { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		public ICollection<Commande> CommandesClient { get; set; }
		
		private bool _isClient;
		private bool _isVendeur;
		private bool _isFournisseur;
		[NotMapped]
		public bool IsClient
		{
			get { return _isClient; }
			set
			{
				if(_isClient != value)
				{
					_isClient = value;
					Type = "C";
					OnPropertyChanged();
				}
			}
		}
		[NotMapped]
		public bool IsVendeur
		{
			get { return _isVendeur; }
			set
			{
				if (_isVendeur != value)
				{
					_isVendeur = value;
					Type = "V";
					OnPropertyChanged();
				}
			}
		}
		[NotMapped]
		public bool IsFournisseur
		{
			get { return _isFournisseur; }
			set
			{
				if (_isFournisseur != value)
				{
					_isFournisseur = value;
					Type = "F";
					OnPropertyChanged();
				}
			}
		}

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
