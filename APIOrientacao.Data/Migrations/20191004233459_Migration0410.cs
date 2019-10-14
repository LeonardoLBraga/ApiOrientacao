using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIOrientacao.Data.Migrations
{
    public partial class Migration0410 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoAluno",
                schema: "dbo",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaAluno",
                schema: "dbo",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoProjeto",
                schema: "dbo",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjetoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_SituacaoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto");

            migrationBuilder.CreateTable(
                name: "Professor",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    RegistroAtivo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdPessoaProfessor", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "FK_PessoaProfessor",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Pessoa",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoOrientacao",
                schema: "dbo",
                columns: table => new
                {
                    IdTipoOrientacao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IdTipoOrientacao", x => x.IdTipoOrientacao);
                });

            migrationBuilder.CreateTable(
                name: "Orientacao",
                schema: "dbo",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(nullable: false),
                    IdProjeto = table.Column<int>(nullable: false),
                    IdTipoOrientacao = table.Column<int>(nullable: false),
                    DataRegistro = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FK_Orientacao", x => new { x.IdProjeto, x.IdPessoa });
                    table.ForeignKey(
                        name: "FK_OrientacaoProfessor",
                        column: x => x.IdPessoa,
                        principalSchema: "dbo",
                        principalTable: "Professor",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrientacaoProjeto",
                        column: x => x.IdProjeto,
                        principalSchema: "dbo",
                        principalTable: "Projeto",
                        principalColumn: "IdProjeto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrientacaoTipoOrientacao",
                        column: x => x.IdTipoOrientacao,
                        principalSchema: "dbo",
                        principalTable: "TipoOrientacao",
                        principalColumn: "IdTipoOrientacao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_IdPessoa",
                schema: "dbo",
                table: "Orientacao",
                column: "IdPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_Orientacao_IdTipoOrientacao",
                schema: "dbo",
                table: "Orientacao",
                column: "IdTipoOrientacao");

            migrationBuilder.AddForeignKey(
                name: "FK_CursoAluno",
                schema: "dbo",
                table: "Aluno",
                column: "IdCurso",
                principalSchema: "dbo",
                principalTable: "Curso",
                principalColumn: "IdCurso",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaAluno",
                schema: "dbo",
                table: "Aluno",
                column: "IdPessoa",
                principalSchema: "dbo",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoProjeto",
                schema: "dbo",
                table: "Projeto",
                column: "IdPessoa",
                principalSchema: "dbo",
                principalTable: "Aluno",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjetoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdProjeto",
                principalSchema: "dbo",
                principalTable: "Projeto",
                principalColumn: "IdProjeto",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SituacaoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdSituacao",
                principalSchema: "dbo",
                principalTable: "Situacao",
                principalColumn: "IdSituacao",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoAluno",
                schema: "dbo",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_PessoaAluno",
                schema: "dbo",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_AlunoProjeto",
                schema: "dbo",
                table: "Projeto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjetoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto");

            migrationBuilder.DropForeignKey(
                name: "FK_SituacaoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto");

            migrationBuilder.DropTable(
                name: "Orientacao",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Professor",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TipoOrientacao",
                schema: "dbo");

            migrationBuilder.AddForeignKey(
                name: "FK_CursoAluno",
                schema: "dbo",
                table: "Aluno",
                column: "IdCurso",
                principalSchema: "dbo",
                principalTable: "Curso",
                principalColumn: "IdCurso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PessoaAluno",
                schema: "dbo",
                table: "Aluno",
                column: "IdPessoa",
                principalSchema: "dbo",
                principalTable: "Pessoa",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoProjeto",
                schema: "dbo",
                table: "Projeto",
                column: "IdPessoa",
                principalSchema: "dbo",
                principalTable: "Aluno",
                principalColumn: "IdPessoa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjetoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdProjeto",
                principalSchema: "dbo",
                principalTable: "Projeto",
                principalColumn: "IdProjeto",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SituacaoSituacoesProjeto",
                schema: "dbo",
                table: "SituacaoProjeto",
                column: "IdSituacao",
                principalSchema: "dbo",
                principalTable: "Situacao",
                principalColumn: "IdSituacao",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
