using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MicroDDD.Infra.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {
        private static HttpClient _httpClient;

        public HttpClient ObterHttpClient()
        {
            return _httpClient ?? (_httpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true }));
        }
    }
}
