using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public class RestHelper : IRestHelper
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ICompactacaoHelper _compactacaoHelper;
        private ConfiguracoesRequisicaoRest _configuracoesRequisicaoRest;

        public RestHelper(IHttpClientHelper httpClientHelper, ICompactacaoHelper compactacaoHelper)
        {
            _httpClientHelper = httpClientHelper;
            _compactacaoHelper = compactacaoHelper;
            _configuracoesRequisicaoRest = new ConfiguracoesRequisicaoRest
            {
                TimeOut = TimeSpan.FromMinutes(5)
            };
        }
        
        public async Task<T> Get<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicaoRest.Get, null);
        }
        public async Task<TR> Post<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicaoRest.Post, valor);
        }

        public async Task<TR> Put<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicaoRest.Put, valor);
        }

        public async Task<T> Delete<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicaoRest.Delete, null);
        }

        private async Task<TR> ObterResponseRetornoRequisicao<T, TR>(string endereco, TipoRequisicaoRest tipoRequisicao, T valor)
        {
            HttpClient client = _httpClientHelper.ObterHttpClient();
            var retornoRequisicao = await ObterRetornoRequisicao(client, endereco, tipoRequisicao, valor);
            if (retornoRequisicao.Dados == null)
            {
                return default(TR);
            }
            return await DesserializaResponse<TR>(retornoRequisicao);
        }

        private async Task<T> DesserializaResponse<T>(ResponseRequisicaoRest retornoRequisicao)
        {
            var json = await ObterJsonResponse(retornoRequisicao);
            var retorno = JsonSerializer.Deserialize<T>(json);
            json = null;
            return retorno;
        }

        private async Task<string> ObterJsonResponse(ResponseRequisicaoRest retornoRequisicao)
        {
            string json = "";
            if (retornoRequisicao.Gzip)
            {
                byte[] descompactado = await _compactacaoHelper.DescompactaGzipAsync(retornoRequisicao.Dados);
                json = Encoding.UTF8.GetString(descompactado, 0, descompactado.Length);
                descompactado = null;
            }
            else
            {
                json = Encoding.UTF8.GetString(retornoRequisicao.Dados, 0, retornoRequisicao.Dados.Length);
            }
            return json;
        }

        private async Task<ResponseRequisicaoRest> ObterRetornoRequisicao<T>(HttpClient client, string endereco, TipoRequisicaoRest tipoRequisicao, 
            T valor)
        {
            try
            {
                using (var response = await ObterResponsePorTipoRequisicao(client, endereco, tipoRequisicao, valor))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new ResponseRequisicaoRest
                        {
                            Dados = await response.Content.ReadAsByteArrayAsync(),
                            Gzip = response.Content.Headers.ContentEncoding.Contains("gzip")
                        };
                    }
                    else
                    {
                        string stringjson = JsonSerializer.Serialize(valor);
                        StringBuilder erro = new StringBuilder();
                        erro.Append("Endereço: ");
                        erro.Append(endereco);
                        erro.Append(Environment.NewLine);
                        erro.Append("Json:");
                        erro.Append(stringjson);
                        erro.Append(Environment.NewLine);
                        erro.Append("Response: ");
                        erro.Append(response.StatusCode);
                        erro.Append(" - ");
                        erro.Append(response.ReasonPhrase);
                        erro.Append(Environment.NewLine);
                        erro.Append("Versão http: ");
                        erro.Append(response.Version);
                        erro.Append(Environment.NewLine);
                        throw new Exception(erro.ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<HttpResponseMessage> ObterResponsePorTipoRequisicao<T>(HttpClient client, string endereco, 
            TipoRequisicaoRest tipoRequisicao, T valor)
        {
            var configuraRequisicao = ConfigurarRequisicao<T>(_configuracoesRequisicaoRest);
            switch (tipoRequisicao)
            {
                case TipoRequisicaoRest.Get:
                    return await client.GetAsync(endereco, _configuracoesRequisicaoRest.TimeOut, configuraRequisicao);
                case TipoRequisicaoRest.Post:
                    var stringjson = JsonSerializer.Serialize(valor);
                    var stringContent = new StringContent(stringjson, Encoding.UTF8, "application/json");
                    return await client.PostAsync(endereco, stringContent, _configuracoesRequisicaoRest.TimeOut, configuraRequisicao);
                case TipoRequisicaoRest.Delete:
                    return await client.DeleteAsync(endereco, _configuracoesRequisicaoRest.TimeOut, configuraRequisicao);
                case TipoRequisicaoRest.Put:
                    stringjson = JsonSerializer.Serialize(valor);
                    stringContent = new StringContent(stringjson, Encoding.UTF8, "application/json");
                    return await client.PutAsync(endereco, stringContent, _configuracoesRequisicaoRest.TimeOut, configuraRequisicao);
                default:
                    return await client.GetAsync(endereco, _configuracoesRequisicaoRest.TimeOut, configuraRequisicao);
            }
        }

        private static Action<HttpRequestMessage> ConfigurarRequisicao<T>(ConfiguracoesRequisicaoRest configuracoesRequisicaoRest)
        {
            return d =>
            {
                if (configuracoesRequisicaoRest.RequestHeaders != null)
                {
                    foreach (var header in configuracoesRequisicaoRest.RequestHeaders)
                    {
                        d.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                    }
                }
                if (configuracoesRequisicaoRest.RequestHeadersNomeValor == null) return;
                {
                    foreach (var header in configuracoesRequisicaoRest.RequestHeadersNomeValor)
                    {
                        d.Headers.Add(header.Key, header.Value);
                    }
                }
                if (!string.IsNullOrEmpty(configuracoesRequisicaoRest.BearerToken))
                {
                    d.Headers.Authorization = new AuthenticationHeaderValue("bearer", configuracoesRequisicaoRest.BearerToken);
                }
                d.Headers.Add("Accept-Encoding", "gzip");
            };
        }        
    }
}
