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
	/// Logique d'interaction pour Accueil.xaml
	/// </summary>
	public partial class Accueil : Window
	{
		public Accueil()
		{
			InitializeComponent();
			ViewModels.AccueilViewModel accvm= new ViewModels.AccueilViewModel();
			DataContext = accvm;
			if (accvm.FermerFenetre == null)
				accvm.FermerFenetre = new Action(this.Close);
		}
    }
}
