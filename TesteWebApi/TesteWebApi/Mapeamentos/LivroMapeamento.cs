using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteWebApi.Entidades;
using Microsoft.EntityFrameworkCore;
using MicroDDD.EntityFramework;

namespace TesteWebApi.Mapeamentos
{
    public class LivroMapeamento : IMapeamento<Livro>
    {
        public void Configura(EntityTypeBuilder<Livro> typeBuilderEntidade)
        {
            typeBuilderEntidade.ToTable("Livro");
        }
    }
}
