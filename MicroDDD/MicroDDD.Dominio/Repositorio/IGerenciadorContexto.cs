using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Dominio.Repositorio
{
    /// <summary>
    /// Gerencia contextos de execução de comandos em uma base de dados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGerenciadorContexto<T>
    {
        /// <summary>
        /// Obtem uma instancia do contexto atual
        /// </summary>
        /// <returns></returns>
        T ObterContextoAtual();
    }
}
