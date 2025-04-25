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
	/// Logique d'interaction pour InterfaceClient.xaml
	/// </summary>
	public partial class InterfaceClient : Window
	{
		public int UtilisateurId { get; set; }

		public InterfaceClient(int utilId)
		{
			UtilisateurId = utilId;
			InitializeComponent();
			ViewModels.InterfaceClientViewModel clientvm = new ViewModels.InterfaceClientViewModel(UtilisateurId);
			DataContext = clientvm;
			if (clientvm.FermerFenetre == null)
				clientvm.FermerFenetre = new Action(this.Close);
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

        }
    }
}
