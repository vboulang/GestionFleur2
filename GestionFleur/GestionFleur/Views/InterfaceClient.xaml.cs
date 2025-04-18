﻿using System;
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
		public InterfaceClient()
		{
			InitializeComponent();
			ViewModels.InterfaceClientViewModel clientvm = new ViewModels.InterfaceClientViewModel();
			DataContext = clientvm;
			//if (clientvm.FermerFenetre == null)
				//clientvm.FermerFenetre = new Action(this.Close);
		}
	}
}
