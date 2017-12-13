using MicroDDD.Infra.Helpers;
using Microsoft.Extensions.DependencyInjection;
using MicroDDD.EntityFramework;

namespace MicroDDD.Infra.IoC
{
    public class ConfiguradorServicos
    {
        private readonly IServiceCollection _services;        
        
        public ConfiguradorServicos(IServiceCollection services)
        {                        
            _services = services;
        }

        public void Configurar()
        {
            _services.AddScoped<ICompactacaoHelper, CompactacaoHelper>();         
            _services.AddScoped<IRestHelper, RestHelper>();
            _services.AddScoped<IQueryableHelper, QueryableHelper>();
            _services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
        }

        public void Registrar<TI, TC>(ServiceLifetime? lifeTime = null)
        {            
            var descriptor = ServiceDescriptor.Describe(typeof(TI), typeof(TC), lifeTime??ServiceLifetime.Scoped);
            _services.Add(descriptor);            
        }
    }
}