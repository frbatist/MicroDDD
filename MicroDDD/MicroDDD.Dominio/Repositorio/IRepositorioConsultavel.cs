using MicroDDD.Dominio.Entidade;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroDDD.Dominio.Repositorio
{
    public interface IRepositorioConsultavel<T> where T : IEntidade
    {
        /// <summary>
        /// Obter entidade T por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ObterPorIdAsync(long id);
        /// <summary>
        /// Obter entidade T por id de maneira assincrona 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T ObterPorId(long id);
        /// <summary>
        /// Obtem um objeto de consulta para o filtro fornecido
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="include">Propriedades incluidas para eager loading</param>
        /// <returns></returns>
        IQueryable<T> ObterTodos(Expression<Func<T, bool>> filtro = null, params Expression<Func<T, object>>[] include);

    }
}
