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
    public class RestHelper
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly ICompactacaoHelper _compactacaoHelper;

        public RestHelper(IHttpClientHelper httpClientHelper, ICompactacaoHelper compactacaoHelper)
        {
            _httpClientHelper = httpClientHelper;
            _compactacaoHelper = compactacaoHelper;
        }

        public async Task<T> Get<T>(string endereco, TimeSpan timeout, string[] requestHeaders = null, IDictionary<string, string> requestHeadersNomeValor = null)
        {
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicao.Get, null, timeout, requestHeaders, requestHeadersNomeValor);
        }
        public async Task<TR> Post<TR, T>(string endereco, TimeSpan timeout, T valor, string[] requestHeaders = null, IDictionary<string, string> requestHeadersNomeValor = null)
        {
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicao.Post, valor, timeout, requestHeaders, requestHeadersNomeValor);
        }

        public async Task<TR> Put<TR, T>(string endereco, TimeSpan timeout, T valor, string[] requestHeaders = null, IDictionary<string, string> requestHeadersNomeValor = null)
        {
            return await ObterResponseRetornoRequisicao<T, TR>(endereco, TipoRequisicao.Put, valor, timeout, requestHeaders, requestHeadersNomeValor);
        }

        public async Task<T> Delete<T>(string endereco, TimeSpan timeout, string[] requestHeaders = null, IDictionary<string, string> requestHeadersNomeValor = null)
        {
            return await ObterResponseRetornoRequisicao<object, T>(endereco, TipoRequisicao.Delete, null, timeout, requestHeaders, requestHeadersNomeValor);
        }

        private async Task<TR> ObterResponseRetornoRequisicao<T, TR>(string endereco, TipoRequisicao tipoRequisicao, T valor, TimeSpan timeout, string[] requestHeaders, 
            IDictionary<string, string> requestHeadersNomeValor)
        {
            if (timeout == null)
                timeout = TimeSpan.FromMinutes(30);
            HttpClient client = _httpClientHelper.ObterHttpClient();
            var retornoRequisicao = await ObterRetornoRequisicao(client, endereco, tipoRequisicao, valor, 
                timeout, requestHeaders, requestHeadersNomeValor);
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
            T valor, TimeSpan timeout, string[] requestHeaders, IDictionary<string, string> requestHeadersNomeValor)
        {
            try
            {
                using (var response = await ObterResponsePorTipoRequisicao(client, endereco, tipoRequisicao, valor, 
                    timeout, requestHeaders, requestHeadersNomeValor))
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
            TipoRequisicao tipoRequisicao, T valor, TimeSpan timeout, string[] requestHeaders, 
            IDictionary<string, string> requestHeadersNomeValor)
        {
//            Action<HttpRequestMessage> asd = new Action<HttpRequestMessage>(
//                d=> d.Properties.
//            
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

        public async Task GravaRetornoApiDescompactadoAsync (string endereco, string[] requestHeaders, string localGravacao)
        {
            byte[] registros = null;
            var client = _httpClientHelper.ObterHttpClient();
            client.Timeout = TimeSpan.FromMinutes(10);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip");

            if (requestHeaders != null)
                foreach (var header in requestHeaders)
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header));
                }

            using (HttpResponseMessage response = client.GetAsync(endereco).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    registros = response.Content.ReadAsByteArrayAsync().Result;
                }
                if (registros == null)
                {
                    return;
                }
            }

            byte[] descompactado = await _compactacaoHelper.DescompactaGzipAsync(registros);
            using (FileStream file = new FileStream(localGravacao, FileMode.Create))
            {
                await file.WriteAsync(descompactado, 0, descompactado.Length);
            }
            registros = null;
            descompactado = null;

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
