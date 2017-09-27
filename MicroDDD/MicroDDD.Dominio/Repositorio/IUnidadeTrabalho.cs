using MicroDDD.Dominio.Entidade;
using System;
using System.Threading.Tasks;

namespace MicroDDD.Dominio.Repositorio
{
    /// <summary>
    /// Escopo de execução de comando sql com um ORM
    /// </summary>
    public interface IUnidadeTrabalho : IDisposable
    {
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
    }
}
