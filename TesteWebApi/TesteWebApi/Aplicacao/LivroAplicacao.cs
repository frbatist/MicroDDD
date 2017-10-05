using MicroDDD.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroDDD.Dominio.Repositorio;

namespace TesteWebApi.Aplicacao
{
    public class LivroAplicacao : AplicacaoBase
    {
        public LivroAplicacao(IUnidadeTrabalho unidadeTrabalho) : base(unidadeTrabalho)
        {            
        }        
    }
}
