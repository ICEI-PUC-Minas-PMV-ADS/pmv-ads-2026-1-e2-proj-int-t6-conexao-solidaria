using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoSolidaria.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoTabelaDoacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Doacoes_DoacaoId",
                table: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Doacoes");

            migrationBuilder.RenameColumn(
                name: "DoacaoId",
                table: "Avaliacoes",
                newName: "OfertaAjudaId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_DoacaoId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_OfertaAjudaId");

            migrationBuilder.AddColumn<int>(
                name: "SolicitacaoId1",
                table: "OfertasAjuda",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertasAjuda_SolicitacaoId1",
                table: "OfertasAjuda",
                column: "SolicitacaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_OfertasAjuda_OfertaAjudaId",
                table: "Avaliacoes",
                column: "OfertaAjudaId",
                principalTable: "OfertasAjuda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertasAjuda_Solicitacoes_SolicitacaoId1",
                table: "OfertasAjuda",
                column: "SolicitacaoId1",
                principalTable: "Solicitacoes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_OfertasAjuda_OfertaAjudaId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertasAjuda_Solicitacoes_SolicitacaoId1",
                table: "OfertasAjuda");

            migrationBuilder.DropIndex(
                name: "IX_OfertasAjuda_SolicitacaoId1",
                table: "OfertasAjuda");

            migrationBuilder.DropColumn(
                name: "SolicitacaoId1",
                table: "OfertasAjuda");

            migrationBuilder.RenameColumn(
                name: "OfertaAjudaId",
                table: "Avaliacoes",
                newName: "DoacaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Avaliacoes_OfertaAjudaId",
                table: "Avaliacoes",
                newName: "IX_Avaliacoes_DoacaoId");

            migrationBuilder.CreateTable(
                name: "Doacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SolicitacaoId = table.Column<int>(type: "int", nullable: false),
                    DataDoacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ItensDoados = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doacoes_AspNetUsers_DoadorId",
                        column: x => x.DoadorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doacoes_Solicitacoes_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_DoadorId",
                table: "Doacoes",
                column: "DoadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Doacoes_SolicitacaoId",
                table: "Doacoes",
                column: "SolicitacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Doacoes_DoacaoId",
                table: "Avaliacoes",
                column: "DoacaoId",
                principalTable: "Doacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
