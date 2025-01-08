using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class LivroLocalVendaConfiguracao : IEntityTypeConfiguration<LivroLocalVenda>
{
    public void Configure(EntityTypeBuilder<LivroLocalVenda> builder)
    {
        builder.ToTable(nameof(LivroLocalVenda));

        builder.HasKey("LivroId", "LocalVendaId");

        builder
            .Property(propriedade => propriedade.Valor)
            .IsRequired()
            .HasColumnType("decimal(19,4)");

        //builder
        //    .HasOne(propriedade => propriedade.Livro)
        //    .WithMany()
        //    .HasForeignKey(e => e.IdLivro);

        //builder
        //    .HasOne(propriedade => propriedade.LocalVenda)
        //    .WithMany()
        //    .HasForeignKey(e => e.IdLocalVenda);
    }
}