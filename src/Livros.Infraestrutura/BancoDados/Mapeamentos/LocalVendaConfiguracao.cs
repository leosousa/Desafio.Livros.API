using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class LocalVendaConfiguracao : IEntityTypeConfiguration<LocalVenda>
{
    public void Configure(EntityTypeBuilder<LocalVenda> builder)
    {
        builder.ToTable(nameof(LocalVenda));

        builder
            .Property(propriedade => propriedade.Descricao)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(LocalVenda.DESCRICAO_MAXIMO_CARACTERES);
    }
}
