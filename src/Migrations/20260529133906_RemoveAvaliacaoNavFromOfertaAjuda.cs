using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoSolidaria.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAvaliacaoNavFromOfertaAjuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertasAjuda_Avaliacoes_AvaliacaoId",
                table: "OfertasAjuda");

            migrationBuilder.DropIndex(
                name: "IX_OfertasAjuda_AvaliacaoId",
                table: "OfertasAjuda");

            migrationBuilder.DropColumn(
                name: "AvaliacaoId",
                table: "OfertasAjuda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvaliacaoId",
                table: "OfertasAjuda",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertasAjuda_AvaliacaoId",
                table: "OfertasAjuda",
                column: "AvaliacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasAjuda_Avaliacoes_AvaliacaoId",
                table: "OfertasAjuda",
                column: "AvaliacaoId",
                principalTable: "Avaliacoes",
                principalColumn: "Id");
        }
    }
}
