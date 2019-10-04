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
    public class PessoaController : Controller
    {
        private readonly Contexto contexto;

        public PessoaController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            PessoaRequest pessoaRequest)
        {

            var pessoa = new Pessoa
            {
                Nome = pessoaRequest.Nome
            };

            contexto.Pessoa.Add(pessoa);
            contexto.SaveChanges();

            var pessoaRetorno = contexto.Pessoa.Where
                (x => x.IdPessoa == pessoa.IdPessoa)
                .FirstOrDefault();

            PessoaResponse response = new PessoaResponse();

            if (pessoaRetorno != null)
            {
                response.IdPessoa = pessoaRetorno.IdPessoa;
                response.Nome = pessoaRetorno.Nome;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        public IActionResult Get(int idPessoa)
        {
            var pessoa = contexto.Pessoa.FirstOrDefault(
                x => x.IdPessoa == idPessoa);

            return StatusCode(pessoa == null
                ? 404 :
                200, new PessoaResponse
                {
                    IdPessoa = pessoa == null ? -1 : pessoa.IdPessoa,
                    Nome = pessoa == null ? "Pessoa não encontrada"
                    : pessoa.Nome
                });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] PessoaRequest pessoaRequest)
        {
            try { 
            var pessoa = contexto.Pessoa.Where(x => x.IdPessoa == id)
                .FirstOrDefault();

            if(pessoa != null)
            {
                pessoa.Nome = pessoaRequest.Nome;
            }
            contexto.Entry(pessoa).State =
                Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var pessoaRetorno = contexto.Pessoa.FirstOrDefault(
                x => x.IdPessoa == id
            );

            PessoaResponse retorno = new PessoaResponse()
            {
                IdPessoa = pessoaRetorno.IdPessoa,
                Nome = pessoaRetorno.Nome
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pessoa = contexto.Pessoa.FirstOrDefault(
            x => x.IdPessoa == id);

            if(pessoa != null)
            {
                contexto.Pessoa.Remove(pessoa);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Pessoa excluído com sucesso");
        }
    }
}