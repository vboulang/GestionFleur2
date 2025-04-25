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
	/// Logique d'interaction pour InterfaceVendeur.xaml
	/// </summary>
	public partial class InterfaceVendeur : Window
	{
		public int UtilisateurId { get; set; }

		public InterfaceVendeur(int utilId)
		{
			UtilisateurId = utilId;
			InitializeComponent();
			ViewModels.InterfaceVendeurViewModel venvm = new ViewModels.InterfaceVendeurViewModel(utilId);
			DataContext = venvm;
			if (venvm.FermerFenetre == null)
				venvm.FermerFenetre = new Action(this.Close);
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}
