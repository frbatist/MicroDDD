using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteWebApi.Entidades
{
    public class Autor : IEntidade
    {
        public long Id { get; set; }
        public string Nome { get; set; }        
    }
}
