using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class InsertLocalVendaLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO [dbo].[LivroLocalVenda]
           ([IdLivro]
           ,[IdLocalVenda]
           ,[Valor])
VALUES
-- Livro 1 (Dom Casmurro)
(1, 1, 110.00), -- Balcão
(1, 2, 100.00), -- Self-service
(1, 3, 80.00),  -- Internet
(1, 4, 115.00), -- Evento

-- Livro 2 (Grande Sertão: Veredas)
(2, 1, 132.00), -- Balcão
(2, 2, 120.00), -- Self-service
(2, 3, 96.00),  -- Internet
(2, 4, 138.00), -- Evento

-- Livro 3 (O Guarani)
(3, 1, 110.00), -- Balcão
(3, 2, 100.00), -- Self-service
(3, 3, 80.00),  -- Internet
(3, 4, 115.00), -- Evento

-- Livro 4 (Macunaíma)
(4, 1, 121.00), -- Balcão
(4, 2, 110.00), -- Self-service
(4, 3, 88.00),  -- Internet
(4, 4, 126.50), -- Evento

-- Livro 5 (Vidas Secas)
(5, 1, 121.00), -- Balcão
(5, 2, 110.00), -- Self-service
(5, 3, 88.00),  -- Internet
(5, 4, 126.50), -- Evento

-- Livro 6 (O Cortiço)
(6, 1, 110.00), -- Balcão
(6, 2, 100.00), -- Self-service
(6, 3, 80.00),  -- Internet
(6, 4, 115.00), -- Evento

-- Livro 7 (Iracema)
(7, 1, 110.00), -- Balcão
(7, 2, 100.00), -- Self-service
(7, 3, 80.00),  -- Internet
(7, 4, 115.00), -- Evento

-- Livro 8 (Memórias Póstumas de Brás Cubas)
(8, 1, 110.00), -- Balcão
(8, 2, 100.00), -- Self-service
(8, 3, 80.00),  -- Internet
(8, 4, 115.00), -- Evento

-- Livro 9 (Capitães da Areia)
(9, 1, 121.00), -- Balcão
(9, 2, 110.00), -- Self-service
(9, 3, 88.00),  -- Internet
(9, 4, 126.50), -- Evento

-- Livro 10 (Sagarana)
(10, 1, 121.00), -- Balcão
(10, 2, 110.00), -- Self-service
(10, 3, 88.00),  -- Internet
(10, 4, 126.50), -- Evento

-- Livro 11 (A Moreninha)
(11, 1, 99.00),  -- Balcão
(11, 2, 90.00),  -- Self-service
(11, 3, 72.00),  -- Internet
(11, 4, 103.50), -- Evento

-- Livro 12 (O Tempo e o Vento)
(12, 1, 121.00), -- Balcão
(12, 2, 110.00), -- Self-service
(12, 3, 88.00),  -- Internet
(12, 4, 126.50), -- Evento

-- Livro 13 (A Hora da Estrela)
(13, 1, 143.00), -- Balcão
(13, 2, 130.00), -- Self-service
(13, 3, 104.00), -- Internet
(13, 4, 149.50), -- Evento

-- Livro 14 (Lavoura Arcaica)
(14, 1, 137.50), -- Balcão
(14, 2, 125.00), -- Self-service
(14, 3, 100.00), -- Internet
(14, 4, 143.75), -- Evento

-- Livro 15 (Quincas Borba)
(15, 1, 110.00), -- Balcão
(15, 2, 100.00), -- Self-service
(15, 3, 80.00),  -- Internet
(15, 4, 115.00), -- Evento

-- Livro 16 (Fogo Morto)
(16, 1, 121.00), -- Balcão
(16, 2, 110.00), -- Self-service
(16, 3, 88.00),  -- Internet
(16, 4, 126.50), -- Evento

-- Livro 17 (São Bernardo)
(17, 1, 115.50), -- Balcão
(17, 2, 105.00), -- Self-service
(17, 3, 84.00),  -- Internet
(17, 4, 120.75), -- Evento

-- Livro 18 (A Paixão Segundo G.H.)
(18, 1, 126.50), -- Balcão
(18, 2, 115.00), -- Self-service
(18, 3, 92.00),  -- Internet
(18, 4, 132.25), -- Evento

-- Livro 19 (Memorial de Aires)
(19, 1, 115.50), -- Balcão
(19, 2, 105.00), -- Self-service
(19, 3, 84.00),  -- Internet
(19, 4, 120.75), -- Evento

-- Livro 20 (O Primo Basílio)
(20, 1, 110.00), -- Balcão
(20, 2, 100.00), -- Self-service
(20, 3, 80.00),  -- Internet
(20, 4, 115.00); -- Evento
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM [dbo].[LivroLocalVenda];
            ");
        }
    }
}
