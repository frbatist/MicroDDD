using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Dominio.Entidade
{
    /// <summary>
    /// Interface com tipo de Id para entidades utilizadas nas consultas em fontes de dados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntidade<T> : IEntidade
    {
        /// <summary>
        /// Id com tipo generico de acordo com a necessidade
        /// </summary>
        T Id { get; set; }
    }

    /// <summary>
    /// Interface básica para entidades utilizadas nas consultas em fontes de dados
    /// </summary>
    public interface IEntidade
    {        
    }
}
