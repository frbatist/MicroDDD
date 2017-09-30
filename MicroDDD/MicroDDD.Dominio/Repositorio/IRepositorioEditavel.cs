using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroDDD.Dominio.Repositorio
{
    public interface IRepositorioEditavel<T> where T : IEntidade
    {
        /// <summary>
        /// Adiciona ou atualiza as entidades informadas
        /// </summary>
        /// <param name="entidades"></param>
        void AdicionarOuAtualizar(params T[] entidades);
        /// <summary>
        /// Adiciona ou atualiza as entidades informadas
        /// </summary>
        /// <param name="entidades"></param>
        /// <returns></returns>
        Task AdicionarOuAtualizarAsync(params T[] entidades);        
        /// <summary>
        /// Remove as entidades informadas
        /// </summary>
        /// <param name="entidades"></param>
        void Remover(params T[] entidades);
        /// <summary>
        /// Remove as entidades informadas
        /// </summary>
        /// <param name="entidades"></param>
        Task RemoverAsync(params T[] entidades);
        /// <summary>
        /// Adiciona entidades em Lote
        /// </summary>
        /// <param name="entidades"></param>
        void AdicionarLote(IEnumerable<T> entidades);
        /// <summary>
        /// Adiciona entidades em Lote
        /// </summary>
        /// <param name="entidades"></param>
        /// <returns></returns>
        Task AdicionarLoteAsync(IEnumerable<T> entidades);
        /// <summary>
        /// Executa comando sql fornecido 
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="comando"></param>
        /// <returns></returns>
        int ExecutaComandoSql(IEnumerable<ParametroComandoSql> parametros, string comando);
        /// <summary>
        /// Executa comando sql fornecido 
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="comando"></param>
        /// <returns></returns>
        Task<int> ExecutaComandoSqlAsync(IEnumerable<ParametroComandoSql> parametros, string comando);
    }
}
