using MicroDDD.Dominio.Identidade;

namespace MicroDDD.Aplicacao.Identidade
{
    public interface IUsuarioAplicacao
    {
        IUsuarioLogado ObterUsuarioLogado();
    }
}
