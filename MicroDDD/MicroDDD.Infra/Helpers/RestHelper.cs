using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MicroDDD.Infra.Helpers
{
    public class ConfiguracoesRequisicaoRest
    {
        public string[] RequestHeaders { get; set; }
        public IDictionary<string, string> RequestHeadersNomeValor { get; set; }
        public TimeSpan TimeOut { get; set; }
        public string BearerToken { get; set; }
    }

    public class RestHelper
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ICompactacaoHelper _compactacaoHelper;
        private ConfiguracoesRequisicaoRest _configuracoesRequisicaoRest;

        public RestHelper(IHttpClientHelper httpClientHelper, ICompactacaoHelper compactacaoHelper)
        {
            _httpClientHelper = httpClientHelper;
            _compactacaoHelper = compactacaoHelper;
        }
        
        public async Task<T> Get<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicao.Get, null);
        }
        public async Task<TR> Post<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicao.Post, valor);
        }

        public async Task<TR> Put<TR, T>(string endereco, T valor, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicao.Put, valor);
        }

        public async Task<T> Delete<T>(string endereco, ConfiguracoesRequisicaoRest configuracoesRequisicaoRest = null)
        {
            _configuracoesRequisicaoRest = configuracoesRequisicaoRest;
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicao.Delete, null);
        }

        private async Task<TR> ObterResponseRetornoRequisicao<T, TR>(string endereco, TipoRequisicao tipoRequisicao, T valor)
        {
            HttpClient client = _httpClientHelper.ObterHttpClient();
            var retornoRequisicao = await ObterRetornoRequisicao(client, endereco, tipoRequisicao, valor);
            if (retornoRequisicao.Dados == null)
            {
                return default(TR);
            }
            return await DesserializaResponse<TR>(retornoRequisicao);
        }

        private async Task<T> DesserializaResponse<T>(ResponseRequisicao retornoRequisicao)
        {
            string json = await ObterJsonResponse(retornoRequisicao);
            T retorno = JsonConvert.DeserializeObject<T>(json);
            json = null;
            return retorno;
        }

        private async Task<string> ObterJsonResponse(ResponseRequisicao retornoRequisicao)
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

        private async Task<ResponseRequisicao> ObterRetornoRequisicao<T>(HttpClient client, string endereco, TipoRequisicao tipoRequisicao, 
            T valor)
        {
            try
            {
                using (var response = await ObterResponsePorTipoRequisicao(client, endereco, tipoRequisicao, valor))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return new ResponseRequisicao
                        {
                            Dados = await response.Content.ReadAsByteArrayAsync(),
                            Gzip = response.Content.Headers.ContentEncoding.Contains("gzip")
                        };
                    }
                    else
                    {
                        string stringjson = JsonConvert.SerializeObject(valor);
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

        public class ResponseRequisicao
        {
            public byte[] Dados { get; set; }
            public bool Gzip { get; set; }
        }

        private async Task<HttpResponseMessage> ObterResponsePorTipoRequisicao<T>(HttpClient client, string endereco, 
            TipoRequisicao tipoRequisicao, T valor)
        {
            var configuraRequisicao = ConfigurarRequisicao<T>();
            switch (tipoRequisicao)
            {
                case TipoRequisicao.Get:
                    return await client.GetAsync(endereco);
                case TipoRequisicao.Post:
                    string stringjson = JsonConvert.SerializeObject(valor);
                    var stringContent = new StringContent(stringjson, Encoding.UTF8, "application/json");
                    return await client.PostAsync(endereco, stringContent);
                case TipoRequisicao.Delete:
                    return await client.DeleteAsync(endereco);
                case TipoRequisicao.Put:
                    stringjson = JsonConvert.SerializeObject(valor);
                    stringContent = new StringContent(stringjson, Encoding.UTF8, "application/json");
                    return await client.PutAsync(endereco, stringContent);
                default:
                    return await client.GetAsync(endereco);
            }
        }

        private static Action<HttpRequestMessage> ConfigurarRequisicao<T>(ConfiguracoesRequisicaoRest configuracoesRequisicaoRest)
        {
            return d =>
            {
                if (configuracoesRequisicaoRest == null) return;
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
                //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");
                //token
            };
        }

        public enum TipoRequisicao
        {
            Get,
            Post,
            Delete,
            Put
        }
    }
}
