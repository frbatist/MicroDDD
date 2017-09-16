using MicroDDD.Dominio.Entidade;
using MicroDDD.Dominio.Repositorio;

namespace MicroDDD.Dominio.Servico
{
    public class ServicoBase : IServico
    {
        protected IRepositorioEscopo _escopo;

        public ServicoBase(IRepositorioEscopo escopo)
        {
            _escopo = escopo;
        }

        public IRepositorio<T> Repositorio<T>() where T : IEntidade
        {
            return _escopo.ObterRepositorio<T>();
        }
    }
}
