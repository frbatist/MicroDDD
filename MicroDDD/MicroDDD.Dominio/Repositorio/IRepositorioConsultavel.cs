using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
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
        ValueTask<T> ObterPorIdAsync<TId>(TId id);
        /// <summary>
        /// Obter entidade T por id de maneira assincrona 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T ObterPorId<TId>(TId id);
        /// <summary>
        /// Obtem um objeto de consulta para o filtro fornecido
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="include">Propriedades incluidas para eager loading</param>
        /// <returns></returns>
        IQueryable<T> ObterTodos(Expression<Func<T, bool>> filtro = null, params Expression<Func<T, object>>[] include);
        /// <summary>
        /// Realiza carga de uma propriedade de navegação (Explicit load)
        /// </summary>
        /// <typeparam name="P">Tipo da propriedade</typeparam>
        /// <param name="entidade"></param>
        /// <param name="propriedadeLista"></param>
        void CarregarReferencia<P>(T entidade, Expression<Func<T, IEnumerable<P>>> propriedadeLista) where P : class, IEntidade;
        /// <summary>
        /// Realiza carga de uma propriedade de navegação (Explicit load)
        /// </summary>
        /// <typeparam name="P">Tipo da propriedade</typeparam>
        /// <param name="entidade"></param>
        /// <param name="propriedade"></param>
        void CarregarReferencia<P>(T entidade, Expression<Func<T, P>> propriedade) where P : class, IEntidade;
    }
}
