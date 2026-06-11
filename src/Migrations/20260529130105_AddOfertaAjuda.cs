using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoSolidaria.Migrations
{
    /// <inheritdoc />
    public partial class AddOfertaAjuda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfertasAjuda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitacaoId = table.Column<int>(type: "int", nullable: false),
                    VoluntarioId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Modalidade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "doacao"),
                    Mensagem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false, defaultValue: "confirmada"),
                    CriadaEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoEntregaUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ObservacoesEntrega = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ConcluidaEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertasAjuda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfertasAjuda_AspNetUsers_VoluntarioId",
                        column: x => x.VoluntarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfertasAjuda_Solicitacoes_SolicitacaoId",
                        column: x => x.SolicitacaoId,
                        principalTable: "Solicitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfertasAjuda_SolicitacaoId",
                table: "OfertasAjuda",
                column: "SolicitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_OfertasAjuda_VoluntarioId",
                table: "OfertasAjuda",
                column: "VoluntarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfertasAjuda");
        }
    }
}
