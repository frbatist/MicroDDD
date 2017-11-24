using MicroDDD.Aplicacao.Identidade;
using System;
using System.Threading.Tasks;

namespace MicroDDD.Aplicacao
{
    public interface IAplicacao : IDisposable
    {
        Task SalvarAlteracoes();
    }
}
