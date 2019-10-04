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
    public class CursoController : Controller
    {
        private readonly Contexto contexto;

        public CursoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CursoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            PessoaRequest cursoRequest)
        {

            var curso = new Curso
            {
                Nome = cursoRequest.Nome
            };

            contexto.Curso.Add(curso);
            contexto.SaveChanges();

            var cursoRetorno = contexto.Curso.Where
                (x => x.IdCurso == curso.IdCurso)
                .FirstOrDefault();

            CursoResponse response = new CursoResponse();

            if (cursoRetorno != null)
            {
                response.IdCurso = cursoRetorno.IdCurso;
                response.Nome = cursoRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idCurso}")]
        [ProducesResponseType(typeof(CursoResponse), 200)]
        public IActionResult Get(int idCurso)
        {
            var curso = contexto.Curso.FirstOrDefault(
                x => x.IdCurso == idCurso);

            return StatusCode(curso == null
                ? 404 :
                200, new PessoaResponse
                {
                    IdPessoa = curso == null ? -1 : curso.IdCurso,
                    Nome = curso == null ? "Curso não encontrada"
                    : curso.Nome
                });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CursoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] CursoRequest cursoRequest)
        {
            try
            {
                var curso = contexto.Curso.Where(x => x.IdCurso == id)
                    .FirstOrDefault();

                if (curso != null)
                {
                    curso.Nome = cursoRequest.Nome;
                }
                contexto.Entry(curso).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var cursoRetorno = contexto.Curso.FirstOrDefault(
                x => x.IdCurso == id
            );

            CursoResponse retorno = new CursoResponse()
            {
                IdCurso = cursoRetorno.IdCurso,
                Nome = cursoRetorno.Nome
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var curso = contexto.Curso.FirstOrDefault(
            x => x.IdCurso == id);

            if (curso != null)
            {
                contexto.Curso.Remove(curso);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Curso excluído com sucesso");
        }
    }
}
