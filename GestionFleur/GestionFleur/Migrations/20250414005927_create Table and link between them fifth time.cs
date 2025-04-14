using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFleur.Migrations
{
    /// <inheritdoc />
    public partial class createTableandlinkbetweenthemfifthtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bouquets",
                columns: table => new
                {
                    BouquetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageCarte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouquets", x => x.BouquetId);
                });

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
                name: "Fleurs",
                columns: table => new
                {
                    FleurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    couleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleurs", x => x.FleurId);
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

            migrationBuilder.CreateTable(
                name: "BouquetFleur",
                columns: table => new
                {
                    BouquetsBouquetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FleursFleurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouquetFleur", x => new { x.BouquetsBouquetId, x.FleursFleurId });
                    table.ForeignKey(
                        name: "FK_BouquetFleur_Bouquets_BouquetsBouquetId",
                        column: x => x.BouquetsBouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouquetFleur_Fleurs_FleursFleurId",
                        column: x => x.FleursFleurId,
                        principalTable: "Fleurs",
                        principalColumn: "FleurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    VendeursId = table.Column<int>(type: "int", nullable: false),
                    VendeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.CommandeId);
                    table.ForeignKey(
                        name: "FK_Commandes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commandes_Vendeurs_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Vendeurs",
                        principalColumn: "VendeurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BouquetCommande",
                columns: table => new
                {
                    BouquetsBouquetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommandesCommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouquetCommande", x => new { x.BouquetsBouquetId, x.CommandesCommandeId });
                    table.ForeignKey(
                        name: "FK_BouquetCommande_Bouquets_BouquetsBouquetId",
                        column: x => x.BouquetsBouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouquetCommande_Commandes_CommandesCommandeId",
                        column: x => x.CommandesCommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeFleur",
                columns: table => new
                {
                    CommandesCommandeId = table.Column<int>(type: "int", nullable: false),
                    FleursFleurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeFleur", x => new { x.CommandesCommandeId, x.FleursFleurId });
                    table.ForeignKey(
                        name: "FK_CommandeFleur_Commandes_CommandesCommandeId",
                        column: x => x.CommandesCommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeFleur_Fleurs_FleursFleurId",
                        column: x => x.FleursFleurId,
                        principalTable: "Fleurs",
                        principalColumn: "FleurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BouquetCommande_CommandesCommandeId",
                table: "BouquetCommande",
                column: "CommandesCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_BouquetFleur_FleursFleurId",
                table: "BouquetFleur",
                column: "FleursFleurId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFleur_FleursFleurId",
                table: "CommandeFleur",
                column: "FleursFleurId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_VendeurId",
                table: "Commandes",
                column: "VendeurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BouquetCommande");

            migrationBuilder.DropTable(
                name: "BouquetFleur");

            migrationBuilder.DropTable(
                name: "CommandeFleur");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Proprietaires");

            migrationBuilder.DropTable(
                name: "Bouquets");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Fleurs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Vendeurs");
        }
    }
}
