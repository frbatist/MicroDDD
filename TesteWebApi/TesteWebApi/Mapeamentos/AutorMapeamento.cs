using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteWebApi.Entidades;

namespace TesteWebApi.Mapeamentos
{
    public static class AutorMapeamento
    {
        public static void Configura(this EntityTypeBuilder<Autor> entidade)
        {
            entidade.ToTable("Autor");
            entidade.Property(d => d.Nome).IsUnicode(false).HasMaxLength(80);            
        }
    }
}
