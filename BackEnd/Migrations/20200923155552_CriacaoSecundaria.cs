using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class CriacaoSecundaria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrecoId",
                table: "Estacionamento",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Estacionamento_PrecoId",
                table: "Estacionamento",
                column: "PrecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estacionamento_Preco_PrecoId",
                table: "Estacionamento",
                column: "PrecoId",
                principalTable: "Preco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estacionamento_Preco_PrecoId",
                table: "Estacionamento");

            migrationBuilder.DropIndex(
                name: "IX_Estacionamento_PrecoId",
                table: "Estacionamento");

            migrationBuilder.DropColumn(
                name: "PrecoId",
                table: "Estacionamento");
        }
    }
}
