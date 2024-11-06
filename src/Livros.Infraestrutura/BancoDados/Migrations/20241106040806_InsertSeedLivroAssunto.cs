using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedLivroAssunto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Associações Livro_Assunto
-- Associações Livro_Assunto
INSERT INTO [dbo].[Livro_Assunto]
           ([LivroId]
           ,[AssuntoId])
     VALUES
           (1, 1),   -- ""Dom Casmurro"" - Romance
           (1, 11),  -- ""Dom Casmurro"" - Existencialismo
           (1, 14),  -- ""Dom Casmurro"" - Psicologia
           (2, 4),   -- ""Grande Sertão: Veredas"" - História
           (2, 5),   -- ""Grande Sertão: Veredas"" - Guerra
           (2, 6),   -- ""Grande Sertão: Veredas"" - Sociedade
           (3, 2),   -- ""O Guarani"" - Aventuras
           (3, 6),   -- ""O Guarani"" - Sociedade
           (3, 42),  -- ""O Guarani"" - Nacionalismo
           (4, 15),  -- ""Macunaíma"" - Mitologia
           (4, 6),   -- ""Macunaíma"" - Sociedade
           (4, 43),  -- ""Macunaíma"" - Resistência
           (5, 24),  -- ""Vidas Secas"" - Poder
           (5, 25),  -- ""Vidas Secas"" - Pobreza
           (5, 6),   -- ""Vidas Secas"" - Sociedade
           (6, 44),  -- ""O Cortiço"" - Naturalismo
           (6, 6),   -- ""O Cortiço"" - Sociedade
           (6, 7),   -- ""O Cortiço"" - Classes sociais
           (7, 1),   -- ""Iracema"" - Romance
           (7, 41),  -- ""Iracema"" - Indigenismo
           (7, 42),  -- ""Iracema"" - Nacionalismo
           (8, 10),  -- ""Memórias Póstumas de Brás Cubas"" - Filosofia
           (8, 14),  -- ""Memórias Póstumas de Brás Cubas"" - Psicologia
           (8, 6),   -- ""Memórias Póstumas de Brás Cubas"" - Sociedade
           (9, 1),   -- ""Capitães da Areia"" - Romance
           (9, 6),   -- ""Capitães da Areia"" - Sociedade
           (9, 46),  -- ""Capitães da Areia"" - Juventude
           (10, 12), -- ""Sagarana"" - Natureza
           (10, 6),  -- ""Sagarana"" - Sociedade
           (10, 44), -- ""Sagarana"" - Naturalismo
           (11, 1),  -- ""A Moreninha"" - Romance
           (11, 46), -- ""A Moreninha"" - Juventude
           (11, 47), -- ""A Moreninha"" - Idealismo
           (12, 4),  -- ""O Tempo e o Vento"" - História
           (12, 6),  -- ""O Tempo e o Vento"" - Sociedade
           (12, 23), -- ""O Tempo e o Vento"" - Família
           (13, 11), -- ""A Hora da Estrela"" - Existencialismo
           (13, 33), -- ""A Hora da Estrela"" - Identidade
           (13, 37), -- ""A Hora da Estrela"" - Isolamento
           (14, 23), -- ""Lavoura Arcaica"" - Família
           (14, 6),  -- ""Lavoura Arcaica"" - Sociedade
           (14, 50), -- ""Lavoura Arcaica"" - Reflexão
           (15, 1),  -- ""Quincas Borba"" - Romance
           (15, 10), -- ""Quincas Borba"" - Filosofia
           (15, 14), -- ""Quincas Borba"" - Psicologia
           (16, 6),  -- ""Fogo Morto"" - Sociedade
           (16, 24), -- ""Fogo Morto"" - Poder
           (16, 48), -- ""Fogo Morto"" - Declínio
           (17, 6),  -- ""São Bernardo"" - Sociedade
           (17, 24), -- ""São Bernardo"" - Poder
           (17, 49), -- ""São Bernardo"" - Ambição
           (18, 11), -- ""A Paixão Segundo G.H."" - Existencialismo
           (18, 50), -- ""A Paixão Segundo G.H."" - Reflexão
           (18, 37), -- ""A Paixão Segundo G.H."" - Isolamento
           (19, 50), -- ""Memorial de Aires"" - Reflexão
           (19, 6),  -- ""Memorial de Aires"" - Sociedade
           (19, 51), -- ""Memorial de Aires"" - Envelhecimento
           (20, 6),  -- ""O Primo Basílio"" - Sociedade
           (20, 34); -- ""O Primo Basílio"" - Realismo

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Livro_Assunto];
            ");
        }
    }
}
