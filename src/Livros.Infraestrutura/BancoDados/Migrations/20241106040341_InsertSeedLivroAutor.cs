using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedLivroAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Associações Livro_Autor
INSERT INTO [dbo].[Livro_Autor]
           ([LivroId]
           ,[AutorId])
     VALUES
           (1, 19),    -- ""Dom Casmurro"" - Machado de Assis
           (2, 20),    -- ""Grande Sertão: Veredas"" - João Guimarães Rosa
           (3, 21),    -- ""O Guarani"" - José de Alencar
           (4, 22),    -- ""Macunaíma"" - Mário de Andrade
           (5, 23),    -- ""Vidas Secas"" - Graciliano Ramos
           (6, 24),    -- ""O Cortiço"" - Aluísio Azevedo
           (7, 21),    -- ""Iracema"" - José de Alencar
           (8, 19),    -- ""Memórias Póstumas de Brás Cubas"" - Machado de Assis
           (9, 25),    -- ""Capitães da Areia"" - Jorge Amado
           (10, 20),   -- ""Sagarana"" - João Guimarães Rosa
           (11, 26),   -- ""A Moreninha"" - Joaquim Manuel de Macedo
           (12, 27),   -- ""O Tempo e o Vento"" - Érico Veríssimo
           (13, 28),   -- ""A Hora da Estrela"" - Clarice Lispector
           (14, 29),   -- ""Lavoura Arcaica"" - Raduan Nassar
           (15, 19),   -- ""Quincas Borba"" - Machado de Assis
           (16, 30),   -- ""Fogo Morto"" - José Lins do Rego
           (17, 23),   -- ""São Bernardo"" - Graciliano Ramos
           (18, 28),   -- ""A Paixão Segundo G.H."" - Clarice Lispector
           (19, 19),   -- ""Memorial de Aires"" - Machado de Assis
           (20, 31);   -- ""O Primo Basílio"" - Eça de Queirós
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Livro_Autor];
            ");
        }
    }
}
