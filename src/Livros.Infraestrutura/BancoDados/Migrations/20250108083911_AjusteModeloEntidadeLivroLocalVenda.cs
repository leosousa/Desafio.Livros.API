using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class AjusteModeloEntidadeLivroLocalVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroLocalVenda",
                table: "LivroLocalVenda");

            migrationBuilder.DropIndex(
                name: "IX_LivroLocalVenda_LivroId",
                table: "LivroLocalVenda");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroLocalVenda",
                table: "LivroLocalVenda",
                columns: new[] { "LivroId", "LocalVendaId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroLocalVenda",
                table: "LivroLocalVenda");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroLocalVenda",
                table: "LivroLocalVenda",
                columns: new[] { "IdLivro", "IdLocalVenda" });

            migrationBuilder.CreateIndex(
                name: "IX_LivroLocalVenda_LivroId",
                table: "LivroLocalVenda",
                column: "LivroId");
        }
    }
}
