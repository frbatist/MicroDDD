using System.Collections.Generic;
using System.Threading.Tasks;
using TesteWebApi.Entidades;

namespace TesteWebApi.Aplicacao
{
    public interface IAutorAplicacao
    {
        Task<IEnumerable<Autor>> ObterTodos();
    }
}