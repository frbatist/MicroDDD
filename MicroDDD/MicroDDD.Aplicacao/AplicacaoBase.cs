using MicroDDD.Aplicacao.Identidade;
using MicroDDD.Dominio.Repositorio;
using System.Threading.Tasks;

namespace MicroDDD.Aplicacao
{
    public class AplicacaoBase : IAplicacao
    {
        protected IUnidadeTrabalho _unidadeTrabalho;

        public AplicacaoBase(IUnidadeTrabalho unidadeTrabalho)
        {
            _unidadeTrabalho = unidadeTrabalho;
        }

        public async Task SalvarAlteracoes()
        {
            await _unidadeTrabalho.FinalizarAsync();
        }

        public void Dispose()
        {
            _unidadeTrabalho.Dispose();
        }
    }
}
