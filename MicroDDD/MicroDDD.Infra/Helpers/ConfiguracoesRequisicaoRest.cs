using System;
using System.Collections.Generic;

namespace MicroDDD.Infra.Helpers
{
    public class ConfiguracoesRequisicaoRest
    {
        public string[] RequestHeaders { get; set; }
        public IDictionary<string, string> RequestHeadersNomeValor { get; set; }
        public TimeSpan TimeOut { get; set; }
        public string BearerToken { get; set; }
    }
}
