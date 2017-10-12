using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteWebApi.Entidades
{
    public class Livro : IEntidade<long>
    {
        public long Id { get; set; }
    }
}
