using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFleur.Migrations
{
    /// <inheritdoc />
    public partial class CreateDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_ClientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Vendeurs_VendeurId",
                table: "Commandes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Proprietaires");

            migrationBuilder.DropTable(
                name: "Vendeurs");

            migrationBuilder.RenameColumn(
                name: "VendeursId",
                table: "Commandes",
                newName: "TypeDePaiement");

            migrationBuilder.AddColumn<double>(
                name: "prixUnitaire",
                table: "Fleurs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "PaiementEffectue",
                table: "Commandes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TotalTransaction",
                table: "Commandes",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UtilisateurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Identifiant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UtilisateurId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes",
                column: "ClientId",
                principalTable: "Utilisateurs",
                principalColumn: "UtilisateurId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Utilisateurs_VendeurId",
                table: "Commandes",
                column: "VendeurId",
                principalTable: "Utilisateurs",
                principalColumn: "UtilisateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_ClientId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Utilisateurs_VendeurId",
                table: "Commandes");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "prixUnitaire",
                table: "Fleurs");

            migrationBuilder.DropColumn(
                name: "PaiementEffectue",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "TotalTransaction",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "TypeDePaiement",
                table: "Commandes",
                newName: "VendeursId");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalTransaction = table.Column<double>(type: "float", nullable: false),
                    TypeDePaiement = table.Column<int>(type: "int", nullable: false),
                    paiementEffectue = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureId);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    FournisseurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.FournisseurId);
                });

            migrationBuilder.CreateTable(
                name: "Proprietaires",
                columns: table => new
                {
                    ProprietaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietaires", x => x.ProprietaireId);
                });

            migrationBuilder.CreateTable(
                name: "Vendeurs",
                columns: table => new
                {
                    VendeurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendeurs", x => x.VendeurId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_ClientId",
                table: "Commandes",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Vendeurs_VendeurId",
                table: "Commandes",
                column: "VendeurId",
                principalTable: "Vendeurs",
                principalColumn: "VendeurId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
