using System;
using System.Collections.Generic;
using System.Text;

namespace APIOrientacao.Data
{
    public class Orientacao
    {
        public int IdPessoa { get; set; }
        public int IdProjeto { get; set; }
        public int IdTipoOrientacao { get; set; }
        public DateTime DataRegistro { get; set; }

        public Projeto Projeto { get; set; }
        public Professor Professor { get; set; }
        public TipoOrientacao TipoOrientacao { get; set; }
    }
}
