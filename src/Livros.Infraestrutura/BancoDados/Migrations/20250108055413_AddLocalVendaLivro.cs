using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalVendaLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalVenda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalVenda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivroLocalVenda",
                columns: table => new
                {
                    IdLivro = table.Column<int>(type: "int", nullable: false),
                    IdLocalVenda = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(19,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroLocalVenda", x => new { x.IdLivro, x.IdLocalVenda });
                    table.ForeignKey(
                        name: "FK_LivroLocalVenda_Livro_IdLivro",
                        column: x => x.IdLivro,
                        principalTable: "Livro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroLocalVenda_LocalVenda_IdLocalVenda",
                        column: x => x.IdLocalVenda,
                        principalTable: "LocalVenda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroLocalVenda_IdLocalVenda",
                table: "LivroLocalVenda",
                column: "IdLocalVenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroLocalVenda");

            migrationBuilder.DropTable(
                name: "LocalVenda");
        }
    }
}
