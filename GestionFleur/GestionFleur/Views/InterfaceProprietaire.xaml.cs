using GestionFleur.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionFleur.Views
{
	/// <summary>
	/// Logique d'interaction pour InterfaceProprietaire.xaml
	/// </summary>
	public partial class InterfaceProprietaire : Window
	{
		public int UtilisateurId { get; set; }
		public InterfaceProprietaire(int utilId)
		{
			UtilisateurId = utilId;
			InitializeComponent();
			ViewModels.InterfaceProprietaireViewModel propvm = new ViewModels.InterfaceProprietaireViewModel(UtilisateurId);
			DataContext = propvm;
			if (propvm.FermerFenetre == null)
				propvm.FermerFenetre = new Action(this.Close);
		}
	}
}
