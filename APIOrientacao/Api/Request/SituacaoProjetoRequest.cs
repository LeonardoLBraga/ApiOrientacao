using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class SituacaoProjetoRequest
    {
        public int IdSituacao { get; set; }
        public int IdProjeto { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
