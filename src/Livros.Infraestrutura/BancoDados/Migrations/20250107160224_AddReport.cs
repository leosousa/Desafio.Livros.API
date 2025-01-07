using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class AddReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW [dbo].[ViewRelatorioLivrosPorAutor] AS
SELECT 
    A.Nome AS NomeAutor,
    L.Titulo AS TituloLivro,
    L.AnoPublicacao,
    STRING_AGG(ASS.Descricao, ', ') AS Assuntos
FROM 
    Autor A
INNER JOIN 
    Livro_Autor LA ON A.Id = LA.AutorId
INNER JOIN 
    Livro L ON LA.LivroId = L.Id
LEFT JOIN 
    Livro_Assunto LAS ON LAS.LivroId = L.Id
INNER JOIN 
    Assunto ASS ON LAS.AssuntoId = ASS.Id
GROUP BY 
    A.Nome, L.Titulo, L.AnoPublicacao;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP VIEW [dbo].[ViewRelatorioLivrosPorAutor];
            ");
        }
    }
}
