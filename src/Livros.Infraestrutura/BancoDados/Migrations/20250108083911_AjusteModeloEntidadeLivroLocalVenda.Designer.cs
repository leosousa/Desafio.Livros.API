﻿// <auto-generated />
using Livros.Infraestrutura.BancoDados;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Livros.Infraestrutura.BancoDados.Migrations
{
    [DbContext(typeof(LivroDbContext))]
    [Migration("20250108083911_AjusteModeloEntidadeLivroLocalVenda")]
    partial class AjusteModeloEntidadeLivroLocalVenda
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Livro_Assunto", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("AssuntoId")
                        .HasColumnType("int");

                    b.HasKey("LivroId", "AssuntoId");

                    b.HasIndex("AssuntoId");

                    b.ToTable("Livro_Assunto");
                });

            modelBuilder.Entity("Livro_Autor", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.HasKey("LivroId", "AutorId");

                    b.HasIndex("AutorId");

                    b.ToTable("Livro_Autor");
                });

            modelBuilder.Entity("Livros.Dominio.DTOs.RelatorioProducaoLiterariaItem", b =>
                {
                    b.Property<int>("AnoPublicacao")
                        .HasColumnType("int");

                    b.Property<string>("Assuntos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeAutor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TituloLivro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable((string)null);

                    b.ToView("ViewRelatorioLivrosPorAutor", (string)null);
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.Assunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Assunto", (string)null);
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Autor", (string)null);
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoPublicacao")
                        .HasColumnType("int");

                    b.Property<int>("Edicao")
                        .HasColumnType("int");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("Livro", (string)null);
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.LivroLocalVenda", b =>
                {
                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.Property<int>("LocalVendaId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("LivroId", "LocalVendaId");

                    b.HasIndex("LocalVendaId");

                    b.ToTable("LivroLocalVenda", (string)null);
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.LocalVenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("LocalVenda", (string)null);
                });

            modelBuilder.Entity("Livro_Assunto", b =>
                {
                    b.HasOne("Livros.Dominio.Entidades.Assunto", null)
                        .WithMany()
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livros.Dominio.Entidades.Livro", null)
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Livro_Autor", b =>
                {
                    b.HasOne("Livros.Dominio.Entidades.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livros.Dominio.Entidades.Livro", null)
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.LivroLocalVenda", b =>
                {
                    b.HasOne("Livros.Dominio.Entidades.Livro", "Livro")
                        .WithMany("LocaisVenda")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livros.Dominio.Entidades.LocalVenda", "LocalVenda")
                        .WithMany()
                        .HasForeignKey("LocalVendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Livro");

                    b.Navigation("LocalVenda");
                });

            modelBuilder.Entity("Livros.Dominio.Entidades.Livro", b =>
                {
                    b.Navigation("LocaisVenda");
                });
#pragma warning restore 612, 618
        }
    }
}
