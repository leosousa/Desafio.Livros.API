using Livros.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Livros.Infraestrutura.BancoDados.Mapeamentos;

public class LivroConfiguracao : IEntityTypeConfiguration<Livro>
{
    public void Configure(EntityTypeBuilder<Livro> builder)
    {
        builder.ToTable(nameof(Livro));

        builder.HasKey(c => c.Id);

        builder
            .Property(propriedade => propriedade.Titulo)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(Livro.TITULO_MAXIMO_CARACTERES);

        builder
            .Property(propriedade => propriedade.Editora)
            .IsRequired()
            .HasColumnType("varchar")
            .HasMaxLength(Livro.EDITORA_MAXIMO_CARACTERES);

        builder
            .Property(propriedade => propriedade.Edicao)
            .IsRequired();

        builder
            .Property(propriedade => propriedade.AnoPublicacao)
            .IsRequired();

        builder
            .HasMany(propriedade => propriedade.Autores)
            .WithMany(autor => autor.Livros)
            .UsingEntity("Livro_Autor",
                autor => autor.HasOne(typeof(Autor)).WithMany().HasForeignKey("AutorId").HasPrincipalKey(nameof(Autor.Id)),
                livro => livro.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Id)),
                join => join.HasKey("LivroId", "AutorId")
            );

        builder
            .HasMany(propriedade => propriedade.Assuntos)
            .WithMany(autor => autor.Livros)
            .UsingEntity("Livro_Assunto",
                assunto => assunto.HasOne(typeof(Assunto)).WithMany().HasForeignKey("AssuntoId").HasPrincipalKey(nameof(Assunto.Id)),
                livro => livro.HasOne(typeof(Livro)).WithMany().HasForeignKey("LivroId").HasPrincipalKey(nameof(Livro.Id)),
                join => join.HasKey("LivroId", "AssuntoId")
            );
    }
}