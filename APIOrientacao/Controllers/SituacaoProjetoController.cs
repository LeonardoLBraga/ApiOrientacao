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
    public class SituacaoProjetoController : Controller
    {
        private readonly Contexto contexto;

        public SituacaoProjetoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            SituacaoProjetoRequest situacaoProjetoRequest)
        {

            var situacaoProjeto = new SituacaoProjeto
            {
                DataRegistro = situacaoProjetoRequest.DataRegistro
            };

            contexto.SituacaoProjeto.Add(situacaoProjeto);
            contexto.SaveChanges();

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.Where
                (x => x.IdSituacao == situacaoProjeto.IdSituacao
                && x.IdProjeto == situacaoProjeto.IdProjeto)
                .FirstOrDefault();

            SituacaoProjetoResponse response = new SituacaoProjetoResponse();

            if (situacaoProjetoRetorno != null)
            {
                response.IdSituacao = situacaoProjetoRetorno.IdSituacao ;
                response.IdProjeto = situacaoProjetoRetorno.IdProjeto;
                response.DataRegistro = situacaoProjetoRetorno.DataRegistro;
            }

            return StatusCode(200, response);
        }


        [HttpGet("{idSituacao}/{idProjeto}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        public IActionResult Get(int idSituacao, int idProjeto)
        {
            var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
                x => x.IdSituacao == idSituacao &&
                x.IdProjeto == idProjeto);

            return StatusCode(situacaoProjeto == null
                ? 404 :
                200, new SituacaoProjetoResponse
                {
                    IdProjeto = situacaoProjeto == null ? -1 : situacaoProjeto.IdProjeto,
                    IdSituacao = situacaoProjeto == null ? -1 : situacaoProjeto.IdSituacao
                });
        }
        [HttpPut("{id}/{id2}")]
        [ProducesResponseType(typeof(SituacaoProjetoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, int id2, [FromBody] SituacaoProjetoRequest situacaoProjetoRequest)
        {
            try
            {
                var situacaoProjeto = contexto.SituacaoProjeto.Where
                    (x => x.IdSituacao == id && x.IdProjeto == id2)
                    .FirstOrDefault();

                if (situacaoProjeto != null)
                {
                    situacaoProjeto.DataRegistro = situacaoProjetoRequest.DataRegistro;
                }
                contexto.Entry(situacaoProjeto).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var situacaoProjetoRetorno = contexto.SituacaoProjeto.FirstOrDefault(
                x => x.IdSituacao == id && x.IdProjeto == id2
            );

            SituacaoProjetoResponse retorno = new SituacaoProjetoResponse()
            {
                IdSituacao = situacaoProjetoRetorno.IdSituacao,
                IdProjeto = situacaoProjetoRetorno.IdProjeto,
                DataRegistro = situacaoProjetoRetorno.DataRegistro
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}/{id2}")]
        public IActionResult Delete(int id, int id2)
        {
            var situacaoProjeto = contexto.SituacaoProjeto.FirstOrDefault(
             x => x.IdSituacao == id && x.IdProjeto == id2);

            if (situacaoProjeto != null)
            {
                contexto.SituacaoProjeto.Remove(situacaoProjeto);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Situacao Projeto excluído com sucesso");
        }
    }
}
