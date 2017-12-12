using MicroDDD.Dominio.Repositorio;
using Microsoft.Extensions.DependencyInjection;

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
