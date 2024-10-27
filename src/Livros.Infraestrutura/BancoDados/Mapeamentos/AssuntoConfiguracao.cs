using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class AssuntoConfiguracao : IEntityTypeConfiguration<Assunto>
{
    public void Configure(EntityTypeBuilder<Assunto> builder)
    {
        builder.ToTable(nameof(Assunto));

        builder.HasKey(c => c.Id);

        builder
           .Property(propriedade => propriedade.Descricao)
           .IsRequired()
           .HasMaxLength(Assunto.ASSUNTO_DESCRICAO_MAXIMO_CARACTERES);
    }
}