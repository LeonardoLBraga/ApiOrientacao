using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIOrientacao.Api.Request
{
    public class AlunoRequest
    {
        public int idPessoa { get; set; }
        public string Matricula { get; set; }
        public bool RegistroAtivo { get; set; }
        public int IdCurso { get; set; }
    }
}
