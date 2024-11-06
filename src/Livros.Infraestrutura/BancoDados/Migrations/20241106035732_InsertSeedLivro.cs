using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Livro] ON

INSERT INTO [dbo].[Livro]
        ([Id]
		,[Titulo]
        ,[Editora]
        ,[Edicao]
        ,[AnoPublicacao])
    VALUES
        (1, 'Dom Casmurro', 'Garnier', 1, 1899),
        (2, 'Grande Sertão: Veredas', 'José Olympio', 1, 1956),
        (3, 'O Guarani', 'Typografia Laemmert', 1, 1857),
        (4, 'Macunaíma', 'Livraria José Olympio', 1, 1928),
        (5, 'Vidas Secas', 'José Olympio', 1, 1938),
        (6, 'O Cortiço', 'B. L. Garnier', 1, 1890),
        (7, 'Iracema', 'Typografia B. L. Garnier', 1, 1865),
        (8, 'Memórias Póstumas de Brás Cubas', 'Revista Brasileira', 1, 1881),
        (9, 'Capitães da Areia', 'Martins', 1, 1937),
        (10, 'Sagarana', 'José Olympio', 1, 1946),
        (11, 'A Moreninha', 'Tipografia Americana', 1, 1844),
        (12, 'O Tempo e o Vento', 'Globo', 1, 1949),
        (13, 'A Hora da Estrela', 'José Olympio', 1, 1977),
        (14, 'Lavoura Arcaica', 'Brasiliense', 1, 1975),
        (15, 'Quincas Borba', 'Garnier', 1, 1891),
        (16, 'Fogo Morto', 'José Olympio', 1, 1943),
        (17, 'São Bernardo', 'José Olympio', 1, 1934),
        (18, 'A Paixão Segundo G.H.', 'Editora do Autor', 1, 1964),
        (19, 'Memorial de Aires', 'Garnier', 1, 1908),
        (20, 'O Primo Basílio', 'Livraria Moré', 1, 1878);

SET IDENTITY_INSERT [dbo].[Livro] OFF
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Livro];"
            );
        }
    }
}
