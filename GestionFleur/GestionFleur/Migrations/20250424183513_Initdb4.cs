using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionFleur.Migrations
{
    /// <inheritdoc />
    public partial class Initdb4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "Fleurs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "Bouquets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fleurs_CommandeId",
                table: "Fleurs",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bouquets_CommandeId",
                table: "Bouquets",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bouquets_Commandes_CommandeId",
                table: "Bouquets",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleurs_Commandes_CommandeId",
                table: "Fleurs",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "CommandeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bouquets_Commandes_CommandeId",
                table: "Bouquets");

            migrationBuilder.DropForeignKey(
                name: "FK_Fleurs_Commandes_CommandeId",
                table: "Fleurs");

            migrationBuilder.DropIndex(
                name: "IX_Fleurs_CommandeId",
                table: "Fleurs");

            migrationBuilder.DropIndex(
                name: "IX_Bouquets_CommandeId",
                table: "Bouquets");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "Fleurs");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "Bouquets");
        }
    }
}
