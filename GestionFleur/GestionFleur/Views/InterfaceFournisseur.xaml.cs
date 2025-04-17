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
	/// Logique d'interaction pour InterfaceFournisseur.xaml
	/// </summary>
	public partial class InterfaceFournisseur : Window
	{
		public InterfaceFournisseur()
		{
			InitializeComponent();
			ViewModels.InscriptionViewModel fourvm = new ViewModels.InscriptionViewModel();
			DataContext = fourvm;
			if (fourvm.FermerFenetre == null)
				fourvm.FermerFenetre = new Action(this.Close);
		}
	}
}
