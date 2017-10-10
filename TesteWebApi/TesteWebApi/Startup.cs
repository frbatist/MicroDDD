using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TesteWebApi.EF;
using Microsoft.EntityFrameworkCore;
using MicroDDD.Infra.IoC;
using TesteWebApi.Entidades;
using MicroDDD.Dominio.Repositorio;
using TesteWebApi.Repositorio;
using TesteWebApi.Aplicacao;
using MicroDDD.EntityFramework;

namespace TesteWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<DbContext, Contexto>();                        
            EntityFrameworkIoc.Configurar(services);            
            var ioC = new ConfiguradorServicos(services);
            ioC.Configurar();
            services.AddScoped<IRepositorio<Autor>, AutorRepositorio>();
            services.AddScoped<IAutorAplicacao, AutorAplicacao>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
