using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public interface IRestHelper
    {
        Task<T> Delete<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null);
        Task<T> Get<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null);
        Task<TR> Post<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null);
        Task<TR> Put<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null);
    }
}