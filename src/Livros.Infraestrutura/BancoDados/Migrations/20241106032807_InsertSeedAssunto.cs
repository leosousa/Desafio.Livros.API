using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedAssunto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Assunto] ON

INSERT INTO [dbo].[Assunto]
(
    [Id]
    ,[Descricao]
)
    VALUES
        (1, 'Romance'),
        (2, 'Aventuras'),
        (3, 'Comédia'),
        (4, 'História'),
        (5, 'Guerra'),
        (6, 'Sociedade'),
        (7, 'Classes sociais'),
        (8, 'Religião'),
        (9, 'Moralidade'),
        (10, 'Filosofia'),
        (11, 'Existencialismo'),
        (12, 'Natureza'),
        (13, 'Justiça'),
        (14, 'Psicologia'),
        (15, 'Mitologia'),
        (16, 'Épico'),
        (17, 'Modernismo'),
        (18, 'Monólogo interior'),
        (19, 'Riqueza'),
        (20, 'Sonho americano'),
        (21, 'Amor'),
        (22, 'Realismo mágico'),
        (23, 'Família'),
        (24, 'Poder'),
        (25, 'Pobreza'),
        (26, 'Redenção'),
        (27, 'Fantasia'),
        (28, 'Amizade'),
        (29, 'Heroísmo'),
        (30, 'Totalitarismo'),
        (31, 'Liberdade'),
        (32, 'Alienação'),
        (33, 'Identidade'),
        (34, 'Realismo'),
        (35, 'Burocracia'),
        (36, 'Adolescência'),
        (37, 'Isolamento'),
        (38, 'Tempo'),
        (39, 'Cultura'),
        (40, 'Linguagem'),
        (41, 'Indigenismo'),
        (42, 'Nacionalismo'),
        (43, 'Resistência'),
        (44, 'Naturalismo'),
        (45, 'Injustiça'),
        (46, 'Juventude'),
        (47, 'Idealismo'),
        (48, 'Declínio'),
        (49, 'Ambição'),
        (50, 'Reflexão'),
        (51, 'Envelhecimento');

SET IDENTITY_INSERT [dbo].[Assunto] OFF
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Assunto];"
            );
        }
    }
}
