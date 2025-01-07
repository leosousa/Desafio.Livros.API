using Livros.Dominio.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class ProducaoLiterariaConfiguracao : IEntityTypeConfiguration<RelatorioProducaoLiterariaItem>
{
    public void Configure(EntityTypeBuilder<RelatorioProducaoLiterariaItem> builder)
    {
        builder
            .ToView("ViewRelatorioLivrosPorAutor")
            .HasNoKey();
    }
}