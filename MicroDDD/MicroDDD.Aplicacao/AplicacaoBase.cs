using MicroDDD.Aplicacao.Identidade;
using MicroDDD.Dominio.Repositorio;
using MicroDDD.Dominio.Entidade;

namespace MicroDDD.Aplicacao
{
    public class AplicacaoBase : IAplicacao
    {
        public IUsuarioAplicacao UsuarioAplicacao { get; set; }
        private IRepositorioEscopo _repositorioEscopo;

        public AplicacaoBase(IRepositorioEscopo repositorioEscopo)
        {
            _repositorioEscopo = repositorioEscopo;
        }

        public IRepositorio<T> Repositorio<T>() where T : IEntidade
        {
            return _repositorioEscopo.ObterRepositorio<T>();
        }

        public void Dispose()
        {
            _repositorioEscopo.Dispose();
        }
    }
}
