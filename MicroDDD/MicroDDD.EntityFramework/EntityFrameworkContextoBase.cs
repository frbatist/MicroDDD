using MicroDDD.Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace MicroDDD.EntityFramework
{
    public class EntityFrameworkContextoBase : DbContext
    {
        protected EntityFrameworkContextoBase()
        {

        }
        public EntityFrameworkContextoBase(DbContextOptions<EntityFrameworkContextoBase> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mapeamentos = Assembly.GetCallingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.Name == "IEntityTypeConfiguration`1"))
                .Select(x => Activator.CreateInstance(x, new object[] { })).ToList();

            foreach (var mapeamento in mapeamentos)
            {
                modelBuilder.ApplyConfiguration((IEntityTypeConfiguration<IEntidade>)mapeamento);
            }
        }
    }
}
