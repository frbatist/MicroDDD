using MicroDDD.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.Dominio.Repositorio
{
    /// <summary>
    /// Repositorio tipado para consultas de dados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositorio<T> : IRepositorioConsultavel<T>, IRepositorioEditavel<T> where T : IEntidade
    {
    }
}
