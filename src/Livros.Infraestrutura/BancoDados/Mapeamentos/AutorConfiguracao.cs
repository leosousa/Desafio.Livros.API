using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class AutorConfiguracao : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable(nameof(Autor));

        builder.HasKey(c => c.Id);

        builder
           .Property(propriedade => propriedade.Nome)
           .IsRequired()
           .HasColumnType("varchar")
           .HasMaxLength(Autor.AUTOR_NOME_MAXIMO_CARACTERES);
    }
}