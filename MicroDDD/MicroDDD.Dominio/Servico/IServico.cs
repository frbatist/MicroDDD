using MicroDDD.Dominio.Entidade;
using MicroDDD.Dominio.Repositorio;

namespace MicroDDD.Dominio.Servico
{
    /// <summary>
    /// Serviço de domínio
    /// </summary>
    public interface IServico
    {
        IRepositorio<T> Repositorio<T>() where T : IEntidade;
    }
}