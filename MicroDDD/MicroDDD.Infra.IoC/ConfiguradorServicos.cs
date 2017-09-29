using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MicroDDD.Infra.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace MicroDDD.Infra.IoC
{
    public class ConfiguradorServicos
    {
        private readonly IServiceCollection _services;
        private readonly IList<TiposRegistrados> _tiposRegistrados;
        
        public ConfiguradorServicos(IServiceCollection services)
        {
            _tiposRegistrados = new List<TiposRegistrados>();
            
            _services = services;
            _services.AddScoped<ICompactacaoHelper, CompactacaoHelper>();
            _tiposRegistrados.Add(
                new TiposRegistrados
                {
                    Interface = typeof(ICompactacaoHelper), 
                    Concreto = typeof(CompactacaoHelper),
                    LifeTime = ServiceLifetime.Scoped
                }
            );
            _services.AddScoped<IRestHelper, RestHelper>();
            _tiposRegistrados.Add(
                new TiposRegistrados
                {
                    Interface = typeof(ICompactacaoHelper), 
                    Concreto = typeof(CompactacaoHelper),
                    LifeTime = ServiceLifetime.Scoped
                }
            );
            _services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
            _tiposRegistrados.Add(
                new TiposRegistrados
                {
                    Interface = typeof(ICompactacaoHelper), 
                    Concreto = typeof(CompactacaoHelper),
                    LifeTime = ServiceLifetime.Singleton
                }
            );
        }

        public void Registrar<TI, TC>(ServiceLifetime? lifeTime = null)
        {
            var tipoExistente = _tiposRegistrados.FirstOrDefault(d => d.Interface == typeof(TI));
            if (tipoExistente != null)
            {
                var descriptorTipoExistente =
                    ServiceDescriptor.Describe(tipoExistente.Interface, tipoExistente.Concreto, tipoExistente.LifeTime);
                _services.Remove(descriptorTipoExistente);
                _tiposRegistrados.Remove(tipoExistente);
            }
            var descriptor = ServiceDescriptor.Describe(typeof(TI), typeof(TC), lifeTime??ServiceLifetime.Scoped);
            _services.Add(descriptor);
            
        }
    }
    public class TiposRegistrados
    {
        public Type Interface { get; set; }
        public Type Concreto { get; set; }
        public ServiceLifetime LifeTime { get; set; }    
    }
}