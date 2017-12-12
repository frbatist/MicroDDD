using MicroDDD.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MicroDDD.EntityFramework
{
    public class EntityFrameworkUnidadeTrabalho : IUnidadeTrabalho
    {
        protected DbContext _contexto;
        private readonly IServiceProvider _services;

        public EntityFrameworkUnidadeTrabalho(DbContext contexto, IServiceProvider services)
        {
            _contexto = contexto;
            _services = services;
        }

        public void Cancelar()
        {
            _contexto.Dispose();
            _contexto = _services.GetRequiredService<DbContext>();
        }

        public Task CancelarAsync()
        {
            return Task.Run(() =>
                {
                    Cancelar();
                }
            );
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public virtual void Finalizar()
        {
            _contexto.SaveChanges();
        }

        public virtual Task FinalizarAsync()
        {
            return _contexto.SaveChangesAsync();
        }
    }
}
