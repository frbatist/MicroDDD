using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroDDD.Dominio.Repositorio
{
    /// <summary>
    /// Escopo de execução de comando sql com um ORM
    /// </summary>
    public interface IRepositorioEscopo : IDisposable
    {
        /// <summary>
        /// Retorna um repositorio do tipo informado, anexado ao escopo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepositorio<T> ObterRepositorio<T>() where T : IEntidade;
        /// <summary>
        /// Retorna um repositorio do tipo informado, anexado ao escopo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<IRepositorio<T>> ObterRepositorioAsync<T>() where T : class, IEntidade;
        /// <summary>
        /// Cancelar todas as operações contidas no escopo atual
        /// </summary>
        void Cancelar();
        /// <summary>
        /// Cancelar todas as operações contidas no escopo atual
        /// </summary>
        Task CancelarAsync();
        /// <summary>
        /// Finaliza a execução e consiste os registros no banco
        /// </summary>
        void Finalizar();
        /// <summary>
        /// Finaliza a execução e consiste os registros no banco
        /// </summary>
        Task FinalizarAsync();
        /// <summary>
        /// Anexa objeto no contexto 
        /// </summary>
        /// <param name="objeto"></param>
        void AnexarObjeto(IEntidade objeto);
        /// <summary>
        /// Anexa objeto no contexto 
        /// </summary>
        /// <param name="objeto"></param>
        Task AnexarObjetoAsync(IEntidade objeto);
    }
}
