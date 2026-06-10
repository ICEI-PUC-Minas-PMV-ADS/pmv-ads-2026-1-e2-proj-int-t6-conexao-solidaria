using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoSolidaria.Migrations
{
    /// <inheritdoc />
    public partial class MigrandoChatParaOfertaAjuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Doacoes_DoacaoId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "DoacaoId",
                table: "Chats",
                newName: "OfertaAjudaId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_DoacaoId",
                table: "Chats",
                newName: "IX_Chats_OfertaAjudaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_OfertasAjuda_OfertaAjudaId",
                table: "Chats",
                column: "OfertaAjudaId",
                principalTable: "OfertasAjuda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_OfertasAjuda_OfertaAjudaId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "OfertaAjudaId",
                table: "Chats",
                newName: "DoacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_OfertaAjudaId",
                table: "Chats",
                newName: "IX_Chats_DoacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Doacoes_DoacaoId",
                table: "Chats",
                column: "DoacaoId",
                principalTable: "Doacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
