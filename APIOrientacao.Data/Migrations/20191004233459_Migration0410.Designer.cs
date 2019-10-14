﻿// <auto-generated />
using System;
using APIOrientacao.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APIOrientacao.Data.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20191004233459_Migration0410")]
    partial class Migration0410
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APIOrientacao.Data.Aluno", b =>
                {
                    b.Property<int>("IdPessoa")
                        .HasColumnName("IdPessoa");

                    b.Property<int>("IdCurso")
                        .HasColumnName("IdCurso");

                    b.Property<string>("Matricula")
                        .IsRequired()
                        .HasColumnName("Matricula")
                        .HasMaxLength(8);

                    b.Property<bool>("RegistroAtivo")
                        .HasColumnName("RegistroAtivo");

                    b.HasKey("IdPessoa")
                        .HasName("IdPessoaAluno");

                    b.HasIndex("IdCurso");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("APIOrientacao.Data.Curso", b =>
                {
                    b.Property<int>("IdCurso")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdCurso")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(150);

                    b.HasKey("IdCurso")
                        .HasName("IdCurso");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("APIOrientacao.Data.Orientacao", b =>
                {
                    b.Property<int>("IdProjeto")
                        .HasColumnName("IdProjeto");

                    b.Property<int>("IdPessoa")
                        .HasColumnName("IdPessoa");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnName("DataRegistro")
                        .HasColumnType("datetime");

                    b.Property<int>("IdTipoOrientacao")
                        .HasColumnName("IdTipoOrientacao");

                    b.HasKey("IdProjeto", "IdPessoa")
                        .HasName("FK_Orientacao");

                    b.HasIndex("IdPessoa");

                    b.HasIndex("IdTipoOrientacao");

                    b.ToTable("Orientacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.Pessoa", b =>
                {
                    b.Property<int>("IdPessoa")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdPessoa")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(300);

                    b.HasKey("IdPessoa")
                        .HasName("IdPessoa");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("APIOrientacao.Data.Professor", b =>
                {
                    b.Property<int>("IdPessoa")
                        .HasColumnName("IdPessoa");

                    b.Property<bool>("RegistroAtivo")
                        .HasColumnName("RegistroAtivo");

                    b.HasKey("IdPessoa")
                        .HasName("IdPessoaProfessor");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("APIOrientacao.Data.Projeto", b =>
                {
                    b.Property<int>("IdProjeto")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdProjeto")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Encerrado")
                        .HasColumnName("Encerrado");

                    b.Property<int>("IdPessoa")
                        .HasColumnName("IdPessoa");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasMaxLength(150);

                    b.Property<decimal>("Nota")
                        .HasColumnName("Nota");

                    b.HasKey("IdProjeto")
                        .HasName("IdProjeto");

                    b.HasIndex("IdPessoa");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("APIOrientacao.Data.Situacao", b =>
                {
                    b.Property<int>("IdSituacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdSituacao")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasMaxLength(100);

                    b.HasKey("IdSituacao")
                        .HasName("IdSituacao");

                    b.ToTable("Situacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.SituacaoProjeto", b =>
                {
                    b.Property<int>("IdProjeto")
                        .HasColumnName("IdProjeto");

                    b.Property<int>("IdSituacao")
                        .HasColumnName("IdSituacao");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnName("DataRegistro")
                        .HasColumnType("datetime");

                    b.HasKey("IdProjeto", "IdSituacao")
                        .HasName("FK_SituacaoProjeto");

                    b.HasIndex("IdSituacao");

                    b.ToTable("SituacaoProjeto");
                });

            modelBuilder.Entity("APIOrientacao.Data.TipoOrientacao", b =>
                {
                    b.Property<int>("IdTipoOrientacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IdTipoOrientacao")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Descricao")
                        .HasMaxLength(300);

                    b.HasKey("IdTipoOrientacao")
                        .HasName("IdTipoOrientacao");

                    b.ToTable("TipoOrientacao");
                });

            modelBuilder.Entity("APIOrientacao.Data.Aluno", b =>
                {
                    b.HasOne("APIOrientacao.Data.Curso", "Curso")
                        .WithMany("Alunos")
                        .HasForeignKey("IdCurso")
                        .HasConstraintName("FK_CursoAluno")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("APIOrientacao.Data.Pessoa", "Pessoa")
                        .WithOne("Aluno")
                        .HasForeignKey("APIOrientacao.Data.Aluno", "IdPessoa")
                        .HasConstraintName("FK_PessoaAluno")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("APIOrientacao.Data.Orientacao", b =>
                {
                    b.HasOne("APIOrientacao.Data.Professor", "Professor")
                        .WithMany("Orientacoes")
                        .HasForeignKey("IdPessoa")
                        .HasConstraintName("FK_OrientacaoProfessor")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("APIOrientacao.Data.Projeto", "Projeto")
                        .WithMany("Orientacoes")
                        .HasForeignKey("IdProjeto")
                        .HasConstraintName("FK_OrientacaoProjeto")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("APIOrientacao.Data.TipoOrientacao", "TipoOrientacao")
                        .WithMany("Orientacoes")
                        .HasForeignKey("IdTipoOrientacao")
                        .HasConstraintName("FK_OrientacaoTipoOrientacao")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("APIOrientacao.Data.Professor", b =>
                {
                    b.HasOne("APIOrientacao.Data.Pessoa", "Pessoa")
                        .WithOne("Professor")
                        .HasForeignKey("APIOrientacao.Data.Professor", "IdPessoa")
                        .HasConstraintName("FK_PessoaProfessor")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("APIOrientacao.Data.Projeto", b =>
                {
                    b.HasOne("APIOrientacao.Data.Aluno", "Aluno")
                        .WithMany("Projetos")
                        .HasForeignKey("IdPessoa")
                        .HasConstraintName("FK_AlunoProjeto")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("APIOrientacao.Data.SituacaoProjeto", b =>
                {
                    b.HasOne("APIOrientacao.Data.Projeto", "Projeto")
                        .WithMany("SituacoesProjeto")
                        .HasForeignKey("IdProjeto")
                        .HasConstraintName("FK_ProjetoSituacoesProjeto")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("APIOrientacao.Data.Situacao", "Situacao")
                        .WithMany("SituacoesProjeto")
                        .HasForeignKey("IdSituacao")
                        .HasConstraintName("FK_SituacaoSituacoesProjeto")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}