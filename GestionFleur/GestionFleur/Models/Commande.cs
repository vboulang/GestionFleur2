using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GestionFleur.Models
{
	public class Commande : INotifyPropertyChanged
	{
		[Key]
		public int CommandeId { get; set; }
		public double TotalTransaction { get; set; }
		public TypeDePaiement TypeDePaiement { get; set; }
		public bool PaiementEffectue { get; set; }
		public event PropertyChangedEventHandler PropertyChanged;
		//Liaison avec la table Bouquet
		public ICollection<FleursCommandes> Fleurs { get; set; }
		public ICollection<BouquetsCommandes> Bouquets { get; set; }

		//Essai pour lister les items de la commande... 
		public ObservableCollection<Fleur> FleursToView => new ObservableCollection<Fleur>(Fleurs?.Select(fc => fc.Fleur).ToList() ?? new List<Fleur>());
		public ObservableCollection<Bouquet> BouquetsToView => new ObservableCollection<Bouquet> (Bouquets?.Select(bc => bc.Bouquet).ToList() ?? new List<Bouquet>());
		/* a ajouter dans la section Fleurs du view
		<GridViewColumn Header="Fleurs" Width="150">
            <GridViewColumn.CellTemplate>
                <DataTemplate>
                    <ItemsControl ItemsSource="{Binding FleursToView}">
                        <ItemsControl.ItemTemplate>
                            <DataTemplate>
                                <TextBlock Text="{Binding Nom, UpdateSourceTrigger=PropertyChanged}" Margin="5"/>
                            </DataTemplate>
                        </ItemsControl.ItemTemplate>
                    </ItemsControl>
                </DataTemplate>
            </GridViewColumn.CellTemplate>
        </GridViewColumn>
		
		a ajouter dans la section bouquet du view
		<GridViewColumn Header="Bouquets" Width="150">
            <GridViewColumn.CellTemplate>
                <DataTemplate>
                    <ItemsControl ItemsSource="{Binding BouquetsToView}">
                        <ItemsControl.ItemTemplate>
                            <DataTemplate>
                                <TextBlock Text="{Binding Nom, UpdateSourceTrigger=PropertyChanged}" Margin="5"/>
                            </DataTemplate>
                        </ItemsControl.ItemTemplate>
                    </ItemsControl>
                </DataTemplate>
            </GridViewColumn.CellTemplate>
        </GridViewColumn>

		*/
		//Alternative pour montrer le contenu de la commande NE FONCTIONNE PAS, 0 partout
		//Fleurs? evalue s'il y a des fleurs. si null, value = 0, sinon, value est la somme
		public int TotalFleurs => Fleurs?.Sum(f => f.quantite) ?? 0;

		public int TotalBouquets => Bouquets?.Sum(b => b.quantite) ?? 0;
		

		[ForeignKey("ClientId")]
		public int ClientId { get; set; }
		public Utilisateur Client { get; set; }

		public bool IsValid()
		{
			return !(TotalTransaction >= 0) && !(TypeDePaiement != null);
		}
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
