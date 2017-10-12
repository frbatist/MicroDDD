using MicroDDD.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using TesteWebApi.Entidades;

namespace TesteWebApi.EF
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurarTodos();
        }
    }
}
