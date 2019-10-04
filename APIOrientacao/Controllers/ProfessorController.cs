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
    public class ProfessorController : Controller
    {
        private readonly Contexto contexto;

        public ProfessorController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody]
            ProfessorRequest professorRequest)
        {

            var professor = new Professor
            {
                RegistroAtivo = professorRequest.RegistroAtivo
            };

            contexto.Professor.Add(professor);
            contexto.SaveChanges();

            var professorRetorno = contexto.Professor.Where
                (x => x.IdPessoa == professor.IdPessoa)
                .FirstOrDefault();

            ProfessorResponse response = new ProfessorResponse();

            if (professorRetorno != null)
            {
                response.IdPessoa = professorRetorno.IdPessoa;
                response.RegistroAtivo = professorRetorno.RegistroAtivo;
            }

            return StatusCode(200, response);
        }

        [HttpGet("{idPessoa}")]
        [ProducesResponseType(typeof(PessoaResponse), 200)]
        public IActionResult Get(int idPessoa)
        {
            var professor = contexto.Professor.FirstOrDefault(
                x => x.IdPessoa == idPessoa);

            return StatusCode(professor == null
                ? 404 :
                200, new ProfessorResponse
                {
                    IdPessoa = professor == null ? -1 : professor.IdPessoa
                });
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProfessorResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(int id, [FromBody] ProfessorRequest professorRequest)
        {
            try
            {
                var professor = contexto.Professor.Where(x => x.IdPessoa == id)
                    .FirstOrDefault();

                if (professor != null)
                {
                    professor.RegistroAtivo = professorRequest.RegistroAtivo;
                }
                contexto.Entry(professor).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;

                contexto.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.InnerException.Message.FirstOrDefault());
            }

            var professorRetorno = contexto.Professor.FirstOrDefault(
                x => x.IdPessoa == id
            );

            ProfessorResponse retorno = new ProfessorResponse()
            {
                IdPessoa = professorRetorno.IdPessoa,
                RegistroAtivo = professorRetorno.RegistroAtivo
            };
            return StatusCode(200, retorno);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = contexto.Professor.FirstOrDefault(
            x => x.IdPessoa == id);

            if (professor != null)
            {
                contexto.Professor.Remove(professor);
                contexto.SaveChanges();
            }

            return StatusCode(200, "Professor excluído com sucesso");
        }
    }
}
