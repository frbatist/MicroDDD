using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicroDDD.Infra.Helpers
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> GetAsync(
            this HttpClient httpClient, string uri, TimeSpan timeout, Action<HttpRequestMessage> preAction)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                preAction(httpRequestMessage);
                return httpClient.SendAsync(httpRequestMessage, cts.Token);
            }
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(
            this HttpClient httpClient, string uri, T value, TimeSpan timeout, Action<HttpRequestMessage> preAction)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var stringjson = JsonConvert.SerializeObject(value);
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
                {
                    Content = new StringContent(stringjson, Encoding.UTF8, "application/json")
                };
                preAction(httpRequestMessage);
                return await httpClient.SendAsync(httpRequestMessage, cts.Token);
            }
        }
        
        public static async Task<HttpResponseMessage> PutAsync<T>(
            this HttpClient httpClient, string uri, T value, TimeSpan timeout, Action<HttpRequestMessage> preAction)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var stringjson = JsonConvert.SerializeObject(value);
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri)
                {
                    Content = new StringContent(stringjson, Encoding.UTF8, "application/json")
                };
                preAction(httpRequestMessage);
                return await httpClient.SendAsync(httpRequestMessage, cts.Token);
            }
        }
        
        public static async Task<HttpResponseMessage> DeleteAsync(
            this HttpClient httpClient, string uri, TimeSpan timeout, Action<HttpRequestMessage> preAction)
        {
            using (var cts = new CancellationTokenSource(timeout))
            {
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
                preAction(httpRequestMessage);
                return await httpClient.SendAsync(httpRequestMessage, cts.Token);
            }
        }
    }
}