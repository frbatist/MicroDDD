using System.Net.Http;

namespace MicroDDD.Infra.Helpers
{
    public interface IHttpClientHelper
    {
        HttpClient ObterHttpClient();
    }
}