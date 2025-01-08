using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertLocalVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[LocalVenda] ON

INSERT INTO [dbo].[LocalVenda]
(
    [Id]
    ,[Descricao]
)
    VALUES
        (1, 'Balcão'),
		(2, 'Self-service'),
		(3, 'Internet'),
		(4, 'Evento');

SET IDENTITY_INSERT [dbo].[LocalVenda] OFF
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[LocalVenda];
            ");
        }
    }
}
