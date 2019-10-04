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
    public class TipoOrientacaoController : Controller
    {
        private readonly Contexto contexto;

        public TipoOrientacaoController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            TipoOrientacaoRequest tipoOrientacaoRequest)
        {

            var tipoOrientacao = new TipoOrientacao
            {
                Descricao = tipoOrientacaoRequest.Descricao
            };

            contexto.TipoOrientacao.Add(tipoOrientacao);
            contexto.SaveChanges();

            var tipoOrientacaoRetorno = contexto.TipoOrientacao.Where
                (x => x.IdTipoOrientacao == tipoOrientacao.IdTipoOrientacao)
                .FirstOrDefault();

            TipoOrientacaoResponse response = new TipoOrientacaoResponse();

            if (tipoOrientacaoRetorno != null)
            {
                response.IdTipoOrientacao = tipoOrientacaoRetorno.IdTipoOrientacao;
                response.Descricao = tipoOrientacaoRetorno.Descricao;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{IdTipoOrientacao}")]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        public IActionResult Get(int idTipoOrientacao)
        {
            var tipoOrientacao = contexto.TipoOrientacao.FirstOrDefault(
                x => x.IdTipoOrientacao == idTipoOrientacao);

            return StatusCode(tipoOrientacao == null
                ? 404 :
                200, new TipoOrientacaoResponse
                {
                    IdTipoOrientacao = tipoOrientacao == null ? -1 : tipoOrientacao.IdTipoOrientacao,
                    Descricao = tipoOrientacao == null ? "Tipo orientacao não encontrada"
                    : tipoOrientacao.Descricao
                });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TipoOrientacaoResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] TipoOrientacaoRequest tipoOrientacaoRequest)
        {
            try
            {
                var tipoOrientacao = contexto.TipoOrientacao.Where(x => x.IdTipoOrientacao == id)
                    .FirstOrDefault();

                if (tipoOrientacao != null)
                {
                    tipoOrientacao.Descricao = tipoOrientacaoRequest.Descricao;
                }
                contexto.Entry(tipoOrientacao).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var tipoOrientacaoRetorno = contexto.TipoOrientacao.FirstOrDefault(
                x => x.IdTipoOrientacao == id
            );

            TipoOrientacaoResponse retorno = new TipoOrientacaoResponse()
            {
                IdTipoOrientacao = tipoOrientacaoRetorno.IdTipoOrientacao,
                Descricao = tipoOrientacaoRetorno.Descricao
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tipoOrientacao = contexto.TipoOrientacao.FirstOrDefault(
            x => x.IdTipoOrientacao == id);

            if (tipoOrientacao != null)
            {
                contexto.TipoOrientacao.Remove(tipoOrientacao);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Tipo de Orientação excluído com sucesso");
        }
    }
}
