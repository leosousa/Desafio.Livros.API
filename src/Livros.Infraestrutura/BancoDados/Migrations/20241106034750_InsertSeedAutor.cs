using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertSeedAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Autor] ON

INSERT INTO [dbo].[Autor]
           ([Id]
		   ,[Nome])
     VALUES
           (1, 'Miguel de Cervantes'),
           (2, 'Liev Tolstói'),
           (3, 'Jane Austen'),
           (4, 'Dante Alighieri'),
           (5, 'Herman Melville'),
           (6, 'Fiódor Dostoiévski'),
           (7, 'Homero'),
           (8, 'James Joyce'),
           (9, 'F. Scott Fitzgerald'),
           (10, 'Gabriel García Márquez'),
           (11, 'Victor Hugo'),
           (12, 'J.R.R. Tolkien'),
           (13, 'George Orwell'),
           (14, 'Franz Kafka'),
           (15, 'Gustave Flaubert'),
           (16, 'J.D. Salinger'),
           (17, 'Thomas Mann'),
           (18, 'Albert Camus'),
           (19, 'Machado de Assis'),
           (20, 'João Guimarães Rosa'),
           (21, 'José de Alencar'),
           (22, 'Mário de Andrade'),
           (23, 'Graciliano Ramos'),
           (24, 'Aluísio Azevedo'),
           (25, 'Jorge Amado'),
           (26, 'Joaquim Manuel de Macedo'),
           (27, 'Érico Veríssimo'),
           (28, 'Clarice Lispector'),
           (29, 'Raduan Nassar'),
           (30, 'José Lins do Rego'),
           (31, 'Eça de Queirós');

SET IDENTITY_INSERT [dbo].[Autor] OFF
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[Autor];"
            );
        }
    }
}
