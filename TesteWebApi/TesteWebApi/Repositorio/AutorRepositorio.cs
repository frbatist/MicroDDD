using MicroDDD.Dominio.Repositorio;
using MicroDDD.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteWebApi.Entidades;
using Microsoft.EntityFrameworkCore;

namespace TesteWebApi.Repositorio
{
    public class AutorRepositorio : EntityFrameworkRepositorio<Autor>, IRepositorio<Autor>
    {
        public AutorRepositorio(DbContext contexto) : base(contexto)
        {
        }
    }
}
