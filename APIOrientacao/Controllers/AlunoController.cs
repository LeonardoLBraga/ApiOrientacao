using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIOrientacao.Api.Request;
using APIOrientacao.Api.Response;
using APIOrientacao.Data;
using APIOrientacao.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace APIOrientacao.Controllers
{
    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
  
            private readonly Contexto contexto;

            public AlunoController(Contexto contexto)
            {
                this.contexto = contexto;
            }

            [HttpPost]
            [ProducesResponseType(typeof(AlunoResponse), 200)]
            [ProducesResponseType(400)]
            public IActionResult Post([FromBody]
            AlunoRequest alunoRequest)
            {

                var aluno = new Aluno
                {
                    IdPessoa = alunoRequest.idPessoa,
                    Matricula = alunoRequest.Matricula,
                    RegistroAtivo = alunoRequest.RegistroAtivo,
                    IdCurso = alunoRequest.IdCurso
                           
                };

                contexto.Aluno.Add(aluno);
                contexto.SaveChanges();

                var alunoRetorno = contexto.Aluno.Where
                    (x => x.IdPessoa == aluno.IdPessoa)
                    .FirstOrDefault();

                AlunoResponse response = new AlunoResponse();

                if (alunoRetorno != null)
                {
                    response.IdPessoa = alunoRetorno.IdPessoa;
                    response.Matricula = alunoRetorno.Matricula;
                    response.RegistroAtivo = alunoRetorno.RegistroAtivo;
                    response.IdCurso = alunoRetorno.IdCurso;
                }

                return StatusCode(200, response);
            }

            [HttpGet("{matricula}")]
            [ProducesResponseType(typeof(AlunoResponse), 200)]
            public IActionResult Get(string matricula)
            {
                var aluno = contexto.Aluno.FirstOrDefault(
                    x => x.Matricula == matricula);

                return StatusCode(aluno == null
                    ? 404 :
                    200, new AlunoResponse
                    {
                        IdPessoa = aluno == null ? -1 : aluno.IdPessoa,
                        Matricula = aluno == null ? "Matricula do aluno não encontrada"
                        : aluno.Matricula
                    });
            }
            [HttpPut("{id}")]
            [ProducesResponseType(typeof(AlunoResponse), 200)]
            [ProducesResponseType(400)]
            [ProducesResponseType(404)]
            public IActionResult Put(int id, [FromBody] AlunoRequest alunoRequest)
            {
                try
                {
                    var aluno = contexto.Aluno.Where(x => x.IdPessoa == id)
                        .FirstOrDefault();

                    if (aluno != null)
                    {
                    aluno.Matricula = alunoRequest.Matricula;
                    
                    }
                    contexto.Entry(aluno).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;

                    contexto.SaveChanges();

                }
                catch (Exception ex)
                {
                    return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
                }

                var alunoRetorno = contexto.Aluno.FirstOrDefault(
                    x => x.IdPessoa == id
                );

                AlunoResponse retorno = new AlunoResponse()
                {
                    IdPessoa = alunoRetorno.IdPessoa,
                    Matricula = alunoRetorno.Matricula,
                    RegistroAtivo = alunoRetorno.RegistroAtivo,
                };
                return StatusCode(200, retorno);

            }

            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var aluno = contexto.Aluno.FirstOrDefault(
                x => x.IdPessoa == id);

                if (aluno != null)
                {
                    contexto.Aluno.Remove(aluno);
                    contexto.SaveChanges();
                }

                return StatusCode(200, "Aluno excluído com sucesso");
            }
    }
}

