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
    public class ProjetoController : Controller
    {
        private readonly Contexto contexto;

        public ProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            ProjetoRequest projetoRequest)
        {

            var projeto = new Projeto
            {
                Nome = projetoRequest.Nome,
                Encerrado = projetoRequest.Encerrado,
                Nota = projetoRequest.Nota,
                IdPessoa = projetoRequest.IdPessoa
            };

            contexto.Projeto.Add(projeto);
            contexto.SaveChanges();

            var projetoRetorno = contexto.Projeto.Where
                (x => x.IdProjeto == projeto.IdProjeto)
                .FirstOrDefault();

            ProjetoResponse response = new ProjetoResponse();

            if (projetoRetorno != null)
            {
                response.IdPessoa = projetoRetorno.IdPessoa;
                response.Encerrado = projetoRetorno.Encerrado;
                response.Nome = projetoRetorno.Nome;
                response.Nota = projetoRetorno.Nota;

            }

            return StatusCode(200, response);
        }

        [HttpGet("{IdProjeto}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        public IActionResult Get(int idprojeto)
        {
            var projeto = contexto.Projeto.FirstOrDefault(
                x => x.IdProjeto == idprojeto);

            return StatusCode(projeto == null
                ? 404 :
                200, new ProjetoResponse
                {
                    IdProjeto = projeto == null ? -1 : projeto.IdProjeto,
                    Nome = projeto == null ? "Nome do projeto não encontrada"
                    : projeto.Nome
                });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ProjetoRequest projetoRequest)
        {
            try
            {
                var projeto = contexto.Projeto.Where(x => x.IdProjeto == id)
                    .FirstOrDefault();

                if (projeto != null)
                {
                    projeto.Nome = projetoRequest.Nome;
                    projeto.Nota = projetoRequest.Nota;
                    projeto.Encerrado = projetoRequest.Encerrado;

                }
                contexto.Entry(projeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var projetoRetorno = contexto.Projeto.FirstOrDefault(
                x => x.IdProjeto == id
            );

            ProjetoResponse retorno = new ProjetoResponse()
            {
                IdProjeto = projetoRetorno.IdProjeto,
                Nome = projetoRetorno.Nome,
                Nota = projetoRetorno.Nota,
                Encerrado = projetoRetorno.Encerrado,
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var projeto = contexto.Projeto.FirstOrDefault(
            x => x.IdProjeto == id);

            if (projeto != null)
            {
                contexto.Projeto.Remove(projeto);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Projeto excluído com sucesso");
        }
    }
}
