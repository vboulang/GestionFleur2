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
		public DbSet<Models.Bouquet> Bouquets { get; set; }
		public DbSet<Models.Utilisateur> Utilisateurs { get; set; }
		public DbSet<Models.Commande> Commandes { get; set; }
		public DbSet<Models.Fleur> Fleurs { get; set; }
		public DbSet<Models.FleursBouquets> FleursBouquets { get; set; }
		public DbSet<Models.BouquetsCommandes> BouquetsCommandes { get; set; }
		public DbSet<Models.FleursCommandes> FleursCommandes { get; set; }

		//Les tables de jointure//

		//Configuration//
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			string connection_string = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False\r\n\t"; 
			string database_name = "GestionFleurDataBase";
			optionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");
		}

		//DataSeed//
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Models.Commande>()
				.HasOne(c => c.Client)
				.WithMany(u => u.CommandesClient)
				.HasForeignKey(c => c.ClientId)
				.OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<Models.Bouquet>().HasKey(b => b.BouquetId);
		}
	}
}
