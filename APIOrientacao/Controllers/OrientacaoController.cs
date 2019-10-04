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
    public class OrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public OrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            OrientacaoRequest orientacaoRequest)
        {

            var orientacao = new Orientacao
            {
                DataRegistro = orientacaoRequest.DataRegistro
            };

            contexto.Orientacao.Add(orientacao);
            contexto.SaveChanges();

            var orientacaoRetorno = contexto.Orientacao.Where
                (x => x.IdPessoa == orientacao.IdPessoa
                && x.IdProjeto == orientacao.IdProjeto)
                .FirstOrDefault();

            OrientacaoResponse response = new OrientacaoResponse();

            if (orientacaoRetorno != null)
            {
                response.IdPessoa = orientacaoRetorno.IdPessoa;
                response.IdProjeto = orientacaoRetorno.IdProjeto;
                response.DataRegistro = orientacaoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }


        [HttpGet("{idPessoa}/{idProjeto}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        public IActionResult Get(int idPessoa, int idProjeto)
        {
            var orientacao = contexto.Orientacao.FirstOrDefault(
                x => x.IdPessoa == idPessoa &&
                x.IdProjeto == idProjeto);

            return StatusCode(orientacao == null
                ? 404 :
                200, new OrientacaoResponse
                {
                    IdProjeto = orientacao == null ? -1 : orientacao.IdProjeto,
                    IdPessoa = orientacao == null ? -1 : orientacao.IdPessoa
                });
        }
        [HttpPut("{id}/{id2}")]
        [ProducesResponseType(typeof(OrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, int id2, [FromBody] OrientacaoRequest orientacaoRequest)
        {
            try
            {
                var orientacao = contexto.Orientacao.Where
                    (x => x.IdPessoa == id && x.IdProjeto == id2)
                    .FirstOrDefault();

                if (orientacao != null)
                {
                    orientacao.DataRegistro = orientacaoRequest.DataRegistro;
                }
                contexto.Entry(orientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var orientacaoRetorno = contexto.Orientacao.FirstOrDefault(
                x => x.IdPessoa == id && x.IdProjeto == id2
            );

            OrientacaoResponse retorno = new OrientacaoResponse()
            {
                IdPessoa = orientacaoRetorno.IdPessoa,
                IdProjeto = orientacaoRetorno.IdProjeto,
                DataRegistro = orientacaoRetorno.DataRegistro
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}/{id2}")]
        public IActionResult Delete(int id, int id2)
        {
            var orientacao = contexto.Orientacao.FirstOrDefault(
             x => x.IdPessoa == id && x.IdProjeto == id2);

            if (orientacao != null)
            {
                contexto.Orientacao.Remove(orientacao);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Orientação excluído com sucesso");
        }
    }
}
