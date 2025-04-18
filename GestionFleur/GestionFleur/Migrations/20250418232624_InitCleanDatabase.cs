using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFleur.Migrations
{
    /// <inheritdoc />
    public partial class InitCleanDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bouquets",
                columns: table => new
                {
                    BouquetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageCarte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouquets", x => x.BouquetId);
                });

            migrationBuilder.CreateTable(
                name: "Fleurs",
                columns: table => new
                {
                    FleurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Couleur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleurs", x => x.FleurId);
                });

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

            migrationBuilder.CreateTable(
                name: "FleursBouquets",
                columns: table => new
                {
                    BouquetFleursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleurId = table.Column<int>(type: "int", nullable: false),
                    BouquetId = table.Column<int>(type: "int", nullable: false),
                    quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleursBouquets", x => x.BouquetFleursId);
                    table.ForeignKey(
                        name: "FK_FleursBouquets_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FleursBouquets_Fleurs_FleurId",
                        column: x => x.FleurId,
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
                    TotalTransaction = table.Column<double>(type: "float", nullable: false),
                    TypeDePaiement = table.Column<int>(type: "int", nullable: false),
                    PaiementEffectue = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    VendeurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.CommandeId);
                    table.ForeignKey(
                        name: "FK_Commandes_Utilisateurs_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId");
                    table.ForeignKey(
                        name: "FK_Commandes_Utilisateurs_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UtilisateurId");
                });

            migrationBuilder.CreateTable(
                name: "BouquetsCommandes",
                columns: table => new
                {
                    BouquetCommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BouquetId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BouquetsCommandes", x => x.BouquetCommandeId);
                    table.ForeignKey(
                        name: "FK_BouquetsCommandes_Bouquets_BouquetId",
                        column: x => x.BouquetId,
                        principalTable: "Bouquets",
                        principalColumn: "BouquetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BouquetsCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FleursCommandes",
                columns: table => new
                {
                    BouquetCommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FleurId = table.Column<int>(type: "int", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false),
                    quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FleursCommandes", x => x.BouquetCommandeId);
                    table.ForeignKey(
                        name: "FK_FleursCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FleursCommandes_Fleurs_FleurId",
                        column: x => x.FleurId,
                        principalTable: "Fleurs",
                        principalColumn: "FleurId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BouquetsCommandes_BouquetId",
                table: "BouquetsCommandes",
                column: "BouquetId");

            migrationBuilder.CreateIndex(
                name: "IX_BouquetsCommandes_CommandeId",
                table: "BouquetsCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_VendeurId",
                table: "Commandes",
                column: "VendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_FleursBouquets_BouquetId",
                table: "FleursBouquets",
                column: "BouquetId");

            migrationBuilder.CreateIndex(
                name: "IX_FleursBouquets_FleurId",
                table: "FleursBouquets",
                column: "FleurId");

            migrationBuilder.CreateIndex(
                name: "IX_FleursCommandes_CommandeId",
                table: "FleursCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_FleursCommandes_FleurId",
                table: "FleursCommandes",
                column: "FleurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BouquetsCommandes");

            migrationBuilder.DropTable(
                name: "FleursBouquets");

            migrationBuilder.DropTable(
                name: "FleursCommandes");

            migrationBuilder.DropTable(
                name: "Bouquets");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Fleurs");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
