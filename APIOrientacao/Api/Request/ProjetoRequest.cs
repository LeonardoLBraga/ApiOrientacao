using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class ProjetoRequest
    {
        public string Nome { get; set; }
        public bool Encerrado { get; set; }
        public decimal Nota { get; set; }

        public int IdPessoa { get; set; }
    }
}
