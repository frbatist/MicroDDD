using MicroDDD.Dominio.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.EntityFramework
{
    public class IoC
    {
        private readonly IServiceCollection _services;

        public IoC(IServiceCollection services)
        {
            _services = services;
        }

        public void Configurar()
        {
            _services.AddScoped<IUnidadeTrabalho, EntityFrameworkUnidadeTrabalho>();
        }
    }
}
