using MicroDDD.Dominio.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroDDD.EntityFramework
{
    public static class EntityFrameworkIoc
    {
        public static void Configurar(IServiceCollection services)
        {
            services.AddScoped<IUnidadeTrabalho, EntityFrameworkUnidadeTrabalho>();
        }
    }
}
