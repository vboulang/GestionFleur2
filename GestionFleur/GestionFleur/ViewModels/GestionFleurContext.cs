using GestionFleur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Windows.Media;

namespace GestionFleur.ViewModels
{
	internal class GestionFleurContext : DbContext
	{
		//Les tables de base//
		public DbSet<Bouquet> Bouquets { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Commande> Commandes { get; set; }
		public DbSet<Facture> Factures { get; set; }
		public DbSet<Fleur> Fleurs { get; set; }
		public DbSet<Fournisseur> Fournisseurs { get; set; }
		public DbSet<Proprietaire> Proprietaires { get; set; }
		public DbSet<Vendeur> Vendeurs { get; set; }

		//Les tables de jointure//

		//Configuration//
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connection_string = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False";
			string database_name = "GestionFleurDataBase";
			optionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
		}

		//DataSeed//
		//Exemple//
		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<Produit>().HasData(
		//		new Produit() { ProduitId = 1, nom = "Produit1", prix = 10.0 },
		//		new Produit() { ProduitId = 2, nom = "Produit2", prix = 20.0 },
		//		new Produit() { ProduitId = 3, nom = "Produit3", prix = 30.0 }
		//	);
		//	modelBuilder.Entity<Utilisateur>().HasData(
		//	new Utilisateur() { UtilisateurId = 1, nom = "Roger", email = "23@gmail.com" }
		//	);
		//	modelBuilder.Entity<Utilisateur>()
		//	.HasMany(u => u.Commandes)
		//	.WithOne(c => c.Utilisateur)
		//	.HasForeignKey(c => c.UtilisateurId);

		//}
	}
}
