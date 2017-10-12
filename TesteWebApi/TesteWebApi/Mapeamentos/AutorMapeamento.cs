using MicroDDD.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteWebApi.Entidades;

namespace TesteWebApi.Mapeamentos
{
    public class AutorMapeamento : IMapeamento<Autor>
    {
        public void Configura(EntityTypeBuilder<Autor> entidade)
        {
            entidade.ToTable("Autor");
            entidade.Property(d => d.Nome).IsUnicode(false).HasMaxLength(80);            
        } 
    }
}
