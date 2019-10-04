using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class OrientacaoRequest
    {
        public int IdPessoa { get; set; }
        public int IdProjeto { get; set; }
        public int IdTipoOrientacao { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
