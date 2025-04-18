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
		public InterfaceProprietaire()
		{
			InitializeComponent();
			ViewModels.InterfaceProprietaireViewModel propvm = new ViewModels.InterfaceProprietaireViewModel();
			DataContext = propvm;
			if (propvm.FermerFenetre == null)
				propvm.FermerFenetre = new Action(this.Close);
		}
	}
}
