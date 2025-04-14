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
		public DbSet<Utilisateur> Utilisateurs { get; set; }
		public DbSet<Commande> Commandes { get; set; }
		public DbSet<Fleur> Fleurs { get; set; }

		//Les tables de jointure//

		//Configuration//
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connection_string = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False";
			string database_name = "GestionFleurDataBase";
			optionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
		}

		//DataSeed//
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Commande>()
				.HasOne(c => c.Client)
				.WithMany(u => u.CommandesClient)
				.HasForeignKey(c => c.ClientId);
			modelBuilder.Entity<Commande>()
				.HasOne(c => c.Vendeur)
				.WithMany(u => u.CommandesASuperviser)
				.HasForeignKey(c => c.VendeurId);
		}
	}
}
